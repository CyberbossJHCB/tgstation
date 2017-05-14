﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace TGServiceInterface
{
	/// <summary>
	/// The type of chat provider
	/// </summary>
	public enum TGChatProvider
	{
		IRC = 0,
		Discord,
	}

	/// <summary>
	/// For setting up authentication no matter the chat provider
	/// </summary>
	[DataContract]
	[KnownType(typeof(TGIRCSetupInfo))]
	[KnownType(typeof(TGDiscordSetupInfo))]
	public class TGChatSetupInfo
	{
		/// <summary>
		/// Constructs a TGChatSetupInfo from optional past data
		/// </summary>
		/// <param name="baseInfo">Optional past data</param>
		/// <param name="numFields">The number of fields in this chat provider</param>
		protected TGChatSetupInfo(TGChatSetupInfo baseInfo, int numFields)
		{
			DataFields = baseInfo != null ? baseInfo.DataFields : new List<string>(numFields);
			if (baseInfo == null)
				for (var I = 0; I < numFields; ++I)
					DataFields.Add(null);
		}

		public TGChatSetupInfo(IList<string> DeserializedData, TGChatProvider provider)
		{
			DataFields = DeserializedData;
			Provider = provider;
		}

		/// <summary>
		/// Should only be checked
		/// </summary>
		[DataMember]
		public TGChatProvider Provider { get; protected set; }

		/// <summary>
		/// Raw access to the data, should not be used
		/// </summary>
		[DataMember]
		public IList<string> DataFields { get; protected set; }
	}

	/// <summary>
	/// Chat provider for IRC
	/// </summary>
	[DataContract]
	public class TGIRCSetupInfo : TGChatSetupInfo
	{
		const int URLIndex = 0;
		const int PortIndex = 1;
		const int NickIndex = 2;
		const int AuthTargetIndex = 3;
		const int AuthMessageIndex = 4;
		const int FieldsLen = 5;

		/// <summary>
		/// Construct IRC setup info from optional generic info
		/// </summary>
		/// <param name="baseInfo">Optional generic info</param>
		public TGIRCSetupInfo(TGChatSetupInfo baseInfo = null) : base(baseInfo, FieldsLen)
		{
			Provider = TGChatProvider.IRC;
			if (baseInfo == null)
			{
				Nickname = "TGS3";
				URL = "irc.rizon.net";
				Port = 6667;
			}
		}
		/// <summary>
		/// The port of the IRC server
		/// </summary>
		public ushort Port {
			get { return Convert.ToUInt16(DataFields[PortIndex]); }
			set { DataFields[PortIndex] = value.ToString();  }
		}
		/// <summary>
		/// The URL of the IRC server
		/// </summary>
		public string URL
		{
			get { return DataFields[URLIndex]; }
			set { DataFields[URLIndex] = value; }
		}
		/// <summary>
		/// The nickname of the IRC bot
		/// </summary>
		public string Nickname
		{
			get { return DataFields[NickIndex]; }
			set { DataFields[NickIndex] = value; }
		}
		/// <summary>
		/// The target for sending authentication messages
		/// </summary>
		public string AuthTarget
		{
			get { return DataFields[AuthTargetIndex]; }
			set { DataFields[AuthTargetIndex] = value; }
		}
		/// <summary>
		/// The authentication message
		/// </summary>
		public string AuthMessage
		{
			get { return DataFields[AuthMessageIndex]; }
			set { DataFields[AuthMessageIndex] = value; }
		}
	}

	[DataContract]
	public class TGDiscordSetupInfo : TGChatSetupInfo
	{
		const int FieldsLen = 0;
		public TGDiscordSetupInfo(TGChatSetupInfo baseInfo = null) : base(baseInfo, FieldsLen)
		{
			Provider = TGChatProvider.IRC;
		}
	}

	/// <summary>
	/// Used internally
	/// </summary>
	[ServiceContract]
	public interface ITGChatBase
	{
		/// <summary>
		/// Set the chat provider info
		/// </summary>
		/// <param name="info"></param>
		[OperationContract]
		string SetProviderInfo(TGChatSetupInfo info);

		/// <summary>
		/// Set the channels to join
		/// </summary>
		/// <param name="channels">The names of the channels to join and broadcast to</param>
		/// <param name="adminchannel">The name of the channel which accepts admin commands. Need not be in the channels parameter</param>
		[OperationContract]
		void SetChannels(string[] channels = null, string adminchannel = null);

		/// <summary>
		/// Checks connection status
		/// </summary>
		/// <returns>true if connected, false otherwise</returns>
		[OperationContract]
		bool Connected();

		/// <summary>
		/// Reconnect to the chat service
		/// </summary>
		/// <returns>null on success, error message on failure</returns>
		[OperationContract]
		string Reconnect();

		/// <summary>
		/// Send a message to the chat service
		/// </summary>
		/// <param name="msg">The message to send</param>
		/// <param name="adminOnly">true if the message should only be sent to the admin channel, false otherwise</param>
		/// <returns></returns>
		[OperationContract]
		string SendMessage(string msg, bool adminOnly = false);
	}

	[ServiceContract]
	public interface ITGChat : ITGChatBase
	{
		/// <summary>
		/// Returns the chat provider info
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		TGChatSetupInfo ProviderInfo();

		//Get channels we are set to connect to, includes the admin channel
		[OperationContract]
		string[] Channels();

		//Get the admin channel
		[OperationContract]
		string AdminChannel();

		/// <summary>
		/// Check if the configuration allows the IRC bot
		/// </summary>
		/// <returns>true if the bot is enabled, false otherwise</returns>
		[OperationContract]
		bool Enabled();

		/// <summary>
		/// Enable or disable the chat provider
		/// </summary>
		/// <param name="enable">true to enable, false to disable</param>
		/// <returns>null on success, connection error message on failure</returns>
		[OperationContract]
		string SetEnabled(bool enable);

		/// <summary>
		/// Print out the users who can use admin restricted commands over IRC from the admin channel
		/// </summary>
		/// <returns>A list of irc nicknames</returns>
		[OperationContract]
		string[] ListAdmins();

		/// <summary>
		/// Set the users who can use admin restricted commands over IRC from the admin channel
		/// </summary>
		/// <param name="nicknames">The list of irc admin nicknames</param>
		[OperationContract]
		void SetAdmins(string[] nicknames);
	}
}
