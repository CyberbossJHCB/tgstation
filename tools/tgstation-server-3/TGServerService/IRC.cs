﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using TGServiceInterface;
using Meebey.SmartIrc4net;


namespace TGServerService
{
	//hunter2
	partial class TGStationServer : ITGIRC
	{
		public static IrcClient irc;
		int reconnectAttempt = 0;

		//Setup the object and autoconnect if necessary
		void InitIRC()
		{
			irc = new IrcClient() { SupportNonRfc = true };
			irc.OnChannelMessage += Irc_OnChannelMessage;
			Connect();
		}

		//For IRC server commands: <nick> <command>
		private void Irc_OnChannelMessage(object sender, IrcEventArgs e)
		{
			var speaker = e.Data.Nick;
			var message = e.Data.Message.Trim();
			var channel = e.Data.Channel;
			var splits = message.Split(' ');
			if (splits[0].ToLower() != irc.Nickname.ToLower())
				return;
			if (splits.Length == 1)
			{
				SendMessage("Hi!");
				return;
			}

			var asList = new List<string>(splits);
			asList.RemoveAt(0);
			var command = asList[0].ToLower();
			asList.RemoveAt(0);

			SendMessageDirect(IrcCommand(command, speaker, channel, asList), channel);
		}
		
		string HasIRCAdmin(string speaker, string channel)
		{
			if (!Properties.Settings.Default.IRCAdmins.Contains(speaker.ToLower()))
				return "You are not authorized to use that command!";
			if (channel.ToLower() != Properties.Settings.Default.IRCAdminChannel.ToLower())
				return "Use this command in the admin channel!";
			return null;
		}

		//Do stuff with words that were spoken to us
		string IrcCommand(string command, string speaker, string channel, IList<string> parameters)
		{
			TGServerService.ActiveService.EventLog.WriteEntry(String.Format("IRC Command from {0}: {1} {2}", speaker, command, String.Join(" ", parameters)));
			switch (command)
			{
				case "check":
					return HasIRCAdmin(speaker, channel) ?? StatusString();
				case "byond":
					if (parameters.Count > 0 && parameters[0].ToLower() == "--staged")
						return GetVersion(true) ?? "None";
					return GetVersion(false) ?? "Uninstalled";
				case "status":
					return HasIRCAdmin(speaker, channel) ?? SendCommand(SCIRCStatus);
			}
			return "Unknown command: " + command;
		}

		//public api
		public void Setup(string url, ushort port, string username, string[] channels, string adminChannel, TGIRCEnableType enabled)
		{
			var Config = Properties.Settings.Default;
			var ServerChange = false;
			if (url != null)
			{
				Config.IRCServer = url;
				ServerChange = true;
			}
			if (port != 0)
			{
				Config.IRCPort = port;
				ServerChange = true;
			}
			if (username != null)
				Config.IRCNick = username;
			if (adminChannel != null)
				Config.IRCAdminChannel = adminChannel;
			var oldchannels = Properties.Settings.Default.IRCChannels;
			if (channels != null)
			{
				var si = new StringCollection();
				si.AddRange(channels);
				if(!si.Contains(Config.IRCAdminChannel))
					si.Add(Config.IRCAdminChannel);
				Config.IRCChannels = si;
			}
			switch (enabled)
			{
				case TGIRCEnableType.Enable:
					Config.IRCEnabled = true;
					break;
				case TGIRCEnableType.Disable:
					Config.IRCEnabled = false;
					break;
				default:
					break;
			}

			if (Connected())
				if (ServerChange)
					Reconnect();
				else
				{
					irc.RfcNick(Config.IRCNick);
					if (channels != null)
					{
						foreach (var I in channels)
						{
							if (!oldchannels.Contains(I))
								irc.RfcJoin(I);
						}
						foreach (var I in oldchannels)
						{
							if (!Config.IRCChannels.Contains(I))
								irc.RfcPart(I);
						}
					}
				}
		}
		//public api
		public string[] Channels()
		{
			return CollectionToArray(Properties.Settings.Default.IRCChannels);
		}
		//public api
		public string[] CollectionToArray(StringCollection sc)
		{
			string[] strArray = new string[sc.Count];
			sc.CopyTo(strArray, 0);
			return strArray;
		}
		//public api
		public string AdminChannel()
		{
			return Properties.Settings.Default.IRCAdminChannel;
		}
		//public api
		public void SetupAuth(string identifyTarget, string identifyCommand, bool required)
		{
			var Config = Properties.Settings.Default;
			if (identifyTarget != null)
				Config.IRCIdentifyTarget = identifyTarget;
			if (identifyCommand != null)
				Config.IRCIdentifyCommand = identifyCommand;
			Config.IRCIdentifyRequired = required;
			if (Connected())
				Login();
		}
		//Joins configured channels
		void JoinChannels()
		{
			foreach (var I in Properties.Settings.Default.IRCChannels)
				irc.RfcJoin(I);
		}
		//runs the login command
		void Login()
		{
			var Config = Properties.Settings.Default;
			if (Config.IRCIdentifyRequired)
				irc.SendMessage(SendType.Message, Config.IRCIdentifyTarget, Config.IRCIdentifyCommand);
		}
		//public api
		public string Connect()
		{
			if (Connected())
				return null;
			var Config = Properties.Settings.Default;
			if (!Config.IRCEnabled)
				return "IRC disabled by config.";
			try
			{
				try
				{
					irc.Connect(Config.IRCServer, Config.IRCPort);
					reconnectAttempt = 0;
				}
				catch (Exception e)
				{
					reconnectAttempt++;
					if (reconnectAttempt <= 5)
					{
						Thread.Sleep(5000); //Reconnecting after 5 seconds.
						return Connect();
					}
					else
					{
						return "IRC server unreachable: " + e.ToString();
					}
				}

				try
				{
					irc.Login(Config.IRCNick, Config.IRCNick);
				}
				catch (Exception e)
				{
					return "Bot name is already taken: " + e.ToString();
				}
				Login();
				JoinChannels();
				new Thread(new ThreadStart(IRCListen)) { IsBackground = true }.Start();
				return null;
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}
		
		//This is the thread that listens for irc messages
		void IRCListen()
		{
			try
			{
				while(Connected())
					irc.Listen();
			}
			catch { }
		}

		//public api
		public string Reconnect()
		{
			Disconnect();
			return Connect();
		}

		//public api
		public void Disconnect()
		{ 
			try
			{
				//because of a bug in smart irc this takes forever and there's nothing we can really do about it 
				//If you want it fixed, get this damn pull request through https://github.com/meebey/SmartIrc4net/pull/31
				irc.Disconnect();
			}
			catch
			{ }
		}
		//public api
		public bool Connected()
		{
			return irc.IsConnected;
		}
		//public api
		public string SendMessage(string message, bool adminOnly = false)
		{
			try
			{
				if (!Connected())
					return "Disconnected.";
				var Config = Properties.Settings.Default;
				if (adminOnly)
					irc.SendMessage(SendType.Message, Config.IRCAdminChannel, message);
				else
					foreach(var I in Config.IRCChannels)
						irc.SendMessage(SendType.Message, I, message);
				TGServerService.ActiveService.EventLog.WriteEntry(String.Format("IRC Send{0}: {1}", adminOnly ? " (ADMIN)" : "", message));
				return null;
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		/// <summary>
		/// Send a message to a channel
		/// </summary>
		/// <param name="message">The message to send</param>
		/// <param name="channel">The channel to send to</param>
		/// <returns></returns>
		string SendMessageDirect(string message, string channel)
		{
			try
			{
				if (!Connected())
					return "Disconnected.";
				irc.SendMessage(SendType.Message, channel, message);
				TGServerService.ActiveService.EventLog.WriteEntry(String.Format("IRC Send ({0}): {1}", channel, message));
				return null;
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		//public api
		public bool Enabled()
		{
			return Properties.Settings.Default.IRCEnabled;
		}

		//public api
		public string[] ListAdmins()
		{
			return CollectionToArray(Properties.Settings.Default.IRCAdmins);
		}

		//public api
		public void SetAdmins(string[] admins)
		{
			var Config = Properties.Settings.Default;
			var si = new StringCollection();
			si.AddRange(admins);
			Config.IRCAdmins = si;
		}
	}
}
