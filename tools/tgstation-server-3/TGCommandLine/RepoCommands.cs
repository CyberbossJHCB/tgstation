﻿using System;
using System.Collections.Generic;
using TGServiceInterface;

namespace TGCommandLine
{
	class RepoCommand : RootCommand
	{
		public RepoCommand()
		{
			Keyword = "repo";
			Children = new Command[] { new RepoSetupCommand(), new RepoUpdateCommand(), new RepoChangelogCommand(), new RepoCommitCommand(), new RepoPushCommand(), new RepoPythonPathCommand(), new RepoSetEmailCommand(), new RepoSetNameCommand(), new RepoSetCredentialsCommand(), new RepoMergePRCommand(), new RepoListPRsCommand() };
		}
		protected override string GetHelpText()
		{
			return "Manage the git repository";
		}
	}

	class RepoSetupCommand : Command
	{
		public RepoSetupCommand()
		{
			Keyword = "setup";
			RequiredParameters = 1;
		}
		public override ExitCode Run(IList<string> parameters)
		{
			if (!Server.GetComponent<ITGRepository>().Setup(parameters[0], parameters.Count > 1 ? parameters[1] : "master"))
			{
				Console.WriteLine("Error: Repo is busy!");
				return ExitCode.ServerError;
			}
			Console.WriteLine("Setting up repo. This will take a while...");
			return ExitCode.Normal;
		}
		protected override string GetArgumentString()
		{
			return "<git-url> [branchname]";
		}
		protected override string GetHelpText()
		{
			return "Clean up everything and clones the repo at git-url with optional branch name";
		}
	}

	class RepoUpdateCommand : Command
	{
		public RepoUpdateCommand()
		{
			Keyword = "update";
			RequiredParameters = 1;
		}
		public override ExitCode Run(IList<string> parameters)
		{
			bool hard;
			switch (parameters[0].ToLower())
			{
				case "hard":
					hard = true;
					break;
				case "merge":
					hard = false;
					break;
				default:
					Console.WriteLine("Invalid parameter: " + parameters[0]);
					return ExitCode.BadCommand;
			}
			var res = Server.GetComponent<ITGRepository>().Update(hard);
			Console.WriteLine(res ?? "Success");
			return res == null ? ExitCode.Normal : ExitCode.ServerError;
		}
		protected override string GetHelpText()
		{
			return "Updates the current branch the repo is on either via a merge or hard reset";
		}
		protected override string GetArgumentString()
		{
			return "<hard|merge>";
		}
	}
	class RepoChangelogCommand : Command
	{
		public RepoChangelogCommand()
		{
			Keyword = "gen-changelog";
		}
		public override ExitCode Run(IList<string> parameters)
		{
			var result = Server.GetComponent<ITGRepository>().GenerateChangelog(out string error);
			Console.WriteLine(error ?? "Success!");
			if (result != null)
				Console.WriteLine(result);
			return error == null ? ExitCode.Normal : ExitCode.ServerError;
		}

		protected override string GetHelpText()
		{
			return "Compiles the html changelog";
		}
	}
	class RepoCommitCommand : Command
	{
		public RepoCommitCommand()
		{
			Keyword = "commit";
		}
		public override ExitCode Run(IList<string> parameters)
		{
			string res;
			if(parameters.Count > 1)
				res = Server.GetComponent<ITGRepository>().Commit(parameters[0]);
			else
				res = Server.GetComponent<ITGRepository>().Commit();
			Console.WriteLine(res ?? "Success");
			return res == null ? ExitCode.Normal : ExitCode.ServerError;
		}
		protected override string GetArgumentString()
		{
			return "[message]";
		}
		protected override string GetHelpText()
		{
			return "Commits all current changes to the repository using the configured identity. By default, uses the automatic changelog compile message";
		}
	}
	class RepoPushCommand : Command
	{
		public RepoPushCommand()
		{
			Keyword = "push";
		}
		public override ExitCode Run(IList<string> parameters)
		{
			var res = Server.GetComponent<ITGRepository>().Push();
			Console.WriteLine(res ?? "Success");
			return res == null ? ExitCode.Normal : ExitCode.ServerError;
		}

		protected override string GetHelpText()
		{
			return "Pushes commits to the origin branch using the configured credentials";
		}
	}
	class RepoSetEmailCommand : Command
	{
		public RepoSetEmailCommand()
		{
			Keyword = "set-email";
			RequiredParameters = 1;
		}
		public override ExitCode Run(IList<string> parameters)
		{
			Server.GetComponent<ITGRepository>().SetCommitterEmail(parameters[0]);
			return ExitCode.Normal;
		}

		protected override string GetArgumentString()
		{
			return "<e-mail>";
		}
		protected override string GetHelpText()
		{
			return "Set the e-mail used for commits";
		}
	}
	class RepoSetNameCommand : Command
	{
		public RepoSetNameCommand()
		{
			Keyword = "set-name";
			RequiredParameters = 1;
		}
		public override ExitCode Run(IList<string> parameters)
		{
			Server.GetComponent<ITGRepository>().SetCommitterName(parameters[0]);
			return ExitCode.Normal;
		}
		protected override string GetArgumentString()
		{
			return "<name>";
		}
		protected override string GetHelpText()
		{
			return "Set the name used for commits";
		}
	}
	class RepoPythonPathCommand : Command
	{
		public RepoPythonPathCommand()
		{
			Keyword = "python-path";
			RequiredParameters = 1;
		}
		public override ExitCode Run(IList<string> parameters)
		{
			Server.GetComponent<ITGRepository>().SetPythonPath(parameters[0]);
			return ExitCode.Normal;
		}
		protected override string GetArgumentString()
		{
			return "<path>";
		}
		protected override string GetHelpText()
		{
			return "Set the path to the folder containing the python 2.7 installation";
		}
	}
	class RepoSetCredentialsCommand : Command
	{
		public RepoSetCredentialsCommand()
		{
			Keyword = "set-credentials";
		}
		public override ExitCode Run(IList<string> parameters)
		{
			Console.WriteLine("Enter username:");
			var user = Console.ReadLine();
			if (user.Length == 0)
			{
				Console.WriteLine("Invalid username!");
				return ExitCode.BadCommand;
			}
			Console.WriteLine("Enter password:");
			var pass = Program.ReadLineSecure();
			if (pass.Length == 0)
			{
				Console.WriteLine("Invalid password!");
				return ExitCode.BadCommand;
			}
			Server.GetComponent<ITGRepository>().SetCredentials(user, pass);
			return ExitCode.Normal;
		}
		protected override string GetArgumentString()
		{
			return "<path>";
		}
		protected override string GetHelpText()
		{
			return "Set the credentials used for pushing commits";
		}
	}

	class RepoMergePRCommand : Command
	{
		public RepoMergePRCommand()
		{
			Keyword = "merge-pr";
			RequiredParameters = 1;
		}

		public override ExitCode Run(IList<string> parameters)
		{
			ushort PR;
			try
			{
				PR = Convert.ToUInt16(parameters[0]);
			}
			catch
			{
				Console.WriteLine("Invalid PR Number!");
				return ExitCode.BadCommand;
			}
			var res = Server.GetComponent<ITGRepository>().MergePullRequest(PR);
			Console.WriteLine(res ?? "Success");
			return res == null ? ExitCode.Normal : ExitCode.ServerError;
		}
		
		protected override string GetArgumentString()
		{
			return "<pr #>";
		}
		protected override string GetHelpText()
		{
			return "Merge the given pull request from the origin repository into the current branch. Only supported with github remotes";
		}
	}

	class RepoListPRsCommand : Command
	{
		public RepoListPRsCommand()
		{
			Keyword = "list-prs";
		}
		protected override string GetHelpText()
		{
			return "Lists currently merge pull requests";
		}
		public override ExitCode Run(IList<string> parameters)
		{
			var data = Server.GetComponent<ITGRepository>().MergedPullRequests(out string error);
			if(data == null)
			{
				Console.WriteLine(error);
				return ExitCode.ServerError;
			}
			if (data.Count == 0)
				Console.WriteLine("None!");
			else
				foreach (var I in data)
				{
					var innerDick = I.Value;
					Console.WriteLine(String.Format("#{0}: {2} by {3} at commit {1}\r\n", I.Key, innerDick["commit"], innerDick["title"], innerDick["author"]));
				}
			return ExitCode.Normal;
		}
	}
}
