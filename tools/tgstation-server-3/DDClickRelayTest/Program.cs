﻿using System;
using System.Collections.Generic;
using System.Threading;
using TGServiceInterface;

namespace TGCommandLine
{

	class Program
	{
		enum ExitCode
		{
			Normal = 0,
			ConnectionError = 1,
			BadCommand = 2,
			ServerError = 3,
		}
		static ExitCode RunCommandLine(string[] args)
		{

			string command = null, param1 = null, param2 = null;
			if(args.Length > 0)
				command = args[0].Trim().ToLower();

			if (args.Length > 1)
				param1 = args[1].Trim().ToLower();

			if (args.Length > 2)
				param2 = args[2].Trim();

			var res = Server.VerifyConnection();
			if (res != null)
			{
				Console.WriteLine("Unable to connect to service!");
				return ExitCode.ConnectionError;
			}

			try
			{
				switch (command)
				{
					case "irc":
						return IRCCommand(param1, param2);
					case "dm":
						return DMCommand(param1, param2);
					case "?":
					case "help":
						ConsoleHelp();
						break;
					default:
						Console.WriteLine("Invalid command: " + command);
						Console.WriteLine("Type '?' or 'help' for available commands.");
						return ExitCode.BadCommand;
				}
			}
			catch
			{
				Console.WriteLine("Connection interrupted!");
				return ExitCode.ConnectionError;
			}
			return ExitCode.Normal;
		}
		static ExitCode DMCommand(string command, string param)
		{
			var DM = Server.GetComponent<ITGCompiler>();
			switch (command)
			{
				case "compile":
					if (DM.Compiling())
					{
						Console.Write("Error: Compilation job already in progress!");
						return ExitCode.ServerError;
					}
					
					if(Server.GetComponent<ITGByond>().GetVersion(false) == null)
					{
						Console.Write("Error: BYOND is not installed!");
						return ExitCode.ServerError;
					}

					if (!DM.Compile())
					{
						Console.WriteLine("Error: Unable to start compilation! If the game directory is not initialized try running 'dm initialize'");
						return ExitCode.ServerError;
					}
					Console.WriteLine("Compile job started");
					if(param == "--wait")
					{
						Console.WriteLine("Awaiting completion...");
						do
						{
							Thread.Sleep(1000);
						} while (DM.Compiling());
						Console.WriteLine(DM.Compiled() ? "Compilation successful" : "Compilation failed!");
					}
					break;
				case "?":
				case "help":
					Console.WriteLine("DM commands:");
					Console.WriteLine();
					Console.WriteLine("compile [--wait]\t-\tStarts a compile/update job optionally waiting for completion.");
					break;
				default:
					Console.WriteLine("Invalid command: " + command);
					Console.WriteLine("Type 'dm help' for available commands");
					return ExitCode.BadCommand;
			}
			return ExitCode.Normal;
		}

		static ExitCode IRCCommand(string command, string param)
		{
			var IRC = Server.GetComponent<ITGIRC>();
			switch (command)
			{
				case "nick":
					if(param == null)
					{
						Console.WriteLine("Missing parameter!");
						return ExitCode.BadCommand;
					}
					IRC.Setup(null, 0, param);
					break;
				case "join":
					if (param == null)
					{
						Console.WriteLine("Missing parameter!");
						return ExitCode.BadCommand;
					}
					var channels = IRC.Channels();
					var found = false;
					var lowerParam = param.ToLower();
					foreach (var I in channels)
					{
						if (I.ToLower() == lowerParam)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						Array.Resize(ref channels, channels.Length + 1);
						channels[channels.Length - 1] = param;
					}
					IRC.Setup(null, 0, null, channels);
					break;
				case "part":
					if (param == null)
					{
						Console.WriteLine("Missing parameter!");
						return ExitCode.BadCommand;
					}
					channels = IRC.Channels();
					lowerParam = param.ToLower();
					var new_channels = new List<string>();
					foreach (var I in channels)
					{
						if (I.ToLower() == lowerParam)
							continue;
						new_channels.Add(I);
					}
					if(new_channels.Count == 0)
					{
						Console.WriteLine("Error: Cannot part from the last channel!");
						return ExitCode.BadCommand;
					}
					IRC.Setup(null, 0, null, new_channels.ToArray());
					break;
				case "announce":
					if (param == null)
					{
						Console.WriteLine("Missing parameter!");
						return ExitCode.BadCommand;
					}
					var res = IRC.SendMessage("SCP: " + param);
					if(res != null)
					{
						Console.WriteLine("Error: " + res);
						return ExitCode.ServerError;
					}
					break;
				case "channels":
					Console.WriteLine("Currently configured channels:");
					Console.WriteLine("\tAdmin Channel: " + IRC.AdminChannel());
					foreach (var I in IRC.Channels())
						Console.WriteLine("\t" + I);
					break;
				case "?":
				case "help":
					Console.WriteLine("IRC commands:");
					Console.WriteLine();
					Console.WriteLine("nick <name>\t-\tSets the IRC nickname");
					Console.WriteLine("join <channel>\t-\tJoins a channel");
					Console.WriteLine("part <channel>\t-\tLeaves a channel");
					Console.WriteLine("announce <message>\t-\tSends a message to all connected channels");
					Console.WriteLine("channels\t-\tList configured channels");
					break;
				default:
					Console.WriteLine("Invalid command: " + command);
					Console.WriteLine("Type 'irc help' for available commands");
					return ExitCode.BadCommand;
			}
			return ExitCode.Normal;
		}

		static void ConsoleHelp()
		{
			Console.WriteLine("/tg/station 13 Server Control Panel:");
			Console.WriteLine("Avaiable commands (type 'help' after command for more info):");
			Console.WriteLine();
			Console.WriteLine("irc\t-\tManage the IRC user");
			Console.WriteLine("dm\t-\tManage compiling the server");
		}

		static int Main(string[] args)
		{
			return (int)RunCommandLine(args);
		}
	}
}