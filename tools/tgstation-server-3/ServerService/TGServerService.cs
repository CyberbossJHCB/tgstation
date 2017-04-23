﻿using System;
using System.IO;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;
using TGServiceInterface;

namespace TGServerService
{
	public partial class TGServerService : ServiceBase
	{
		public static TGServerService ActiveService;

		ServiceHost host;
		
		public TGServerService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			ActiveService = this;
			try
			{
				var Config = Properties.Settings.Default;

				if (args.Length >= 2 && args[0].ToLower() == "--directory")
				{
					EventLog.WriteEntry("Commandline setting directory to: " + args[1]);
					Config.ServerDirectory = args[1];
				}

				if (!Directory.Exists(Config.ServerDirectory))
				{
					EventLog.WriteEntry("Creating server directory: " + Config.ServerDirectory);
					Directory.CreateDirectory(Config.ServerDirectory);
				}
				Directory.SetCurrentDirectory(Config.ServerDirectory);
				
				host = new ServiceHost(typeof(TGStationServer), new Uri[] { new Uri("net.pipe://localhost") });

				AddEndpoint<ITGRepository>();
				AddEndpoint<ITGByond>();
				AddEndpoint<ITGCompiler>();

				host.Open();
			}
			catch
			{
				ActiveService = null;
				throw;
			}
		}

		void AddEndpoint<T>()
		{
			var typetype = typeof(T);
			host.AddServiceEndpoint(typetype, new NetNamedPipeBinding(), Server.MasterPipeName + "/" + typetype.Name);
		}

		protected override void OnStop()
		{
			try
			{
				host.Close();
				host = null;


			}
			finally
			{
				Properties.Settings.Default.Save();
				ActiveService = null;
			}
		}
	}
}
