﻿using System.Collections.Generic;
using System.ServiceModel;

namespace TGServiceInterface
{
	//for managing the code repository
	//note, the only way to run git clean on the repo is to run Setup again
	[ServiceContract]
	public interface ITGRepository
	{
		//returns true if something is using the repo
		//calls are serialized, so calling a git operation function if this is true will cause it to block until the previous operation completes
		[OperationContract]
		bool OperationInProgress();

		//returns the current progress of a setup, update, or checkout operation
		[OperationContract]
		int CheckoutProgress();

		//check if the repository is valid, if not Setup must be called
		[OperationContract]
		bool Exists();

		//Deletes whatever may be left over and clones the repo at remote and checks out branch master
		//runs asyncronously, use IsBusy, GetProgress, and Exists to check status
		[OperationContract]
		void Setup(string remote, string branch = "master");

		//returns the sha of the current HEAD
		//if null, error will contain the error
		[OperationContract]
		string GetHead(out string error);

		//returns the name of the current branch
		//if null, error will contain the error
		[OperationContract]
		string GetBranch(out string error);

		//hard checks outW the passed branch or sha
		//returns null on success, error message on failure
		[OperationContract]
		string Checkout(string branchorsha);

		//Fetches the origin and hard resets the current branch
		//returns null on success, error message on failure
		[OperationContract]
		string Update();

		//Hard resets the current branch
		//returns null on success, error message on failure
		[OperationContract]
		string Reset();

		//Merges the pull request number if the remote is a github repository
		//returns null on success, error message on failure
		[OperationContract]
		string MergePullRequest(int PRnumber);

		//Returns a list of PR# -> Sha of the currently merged pull requests
		//returns null on failure and error will be set
		[OperationContract]
		IDictionary<string, string> MergedPullRequests(out string error);

		//Gets the name of the current git committer
		[OperationContract]
		string GetCommitterName();

		//Sets the name of the current git committer
		[OperationContract]
		void SetCommitterName(string newName);
		//Gets the name of the current git email
		[OperationContract]
		string GetCommitterEmail();

		//Sets the name of the current git email
		[OperationContract]
		void SetCommitterEmail(string newEmail);

		//Gets the username of the current git credentials
		[OperationContract]
		string GetCredentialUsername();

		//Sets the git remote credentials
		[OperationContract]
		void SetCredentials(string username, string password);

		//Equivalent to running git commit -a -m '<message>' with the set git identity
		//returns null on success, error on failure
		[OperationContract]
		string Commit(string message);

		//pushes the current branch to origin with the set git credentials
		//returns null on success, error on failure
		[OperationContract]
		string Push();
	}
}
