using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace OpenKit.Native
{
	public class OpenKitIOS : IOKNativeBridge
	{
		public const string OK_IPHONE_DLL = "__Internal";

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSetAppKey(string appKey);

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSetSecretKey(string secretKey);

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSetEndpoint(string endpoint);
		
		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSetLeaderboardListTag(string tag);

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeShowLeaderboards();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeShowLeaderboardsLandscapeOnly();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeShowLoginUI();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSubmitScoreWithGameCenter(Int64 scoreValue, int leaderboardID, int metadata, string displayString, string gameObjectName, string gamecenterLeaderboardID);

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeSubmitScore(Int64 scoreValue, int leaderboardID, int metadata, string displayString, string gameObjectName);

		[DllImport (OK_IPHONE_DLL)]
		public static extern int OKBridgeGetCurrentUserOKID();

		[DllImport (OK_IPHONE_DLL)]
		public static extern string OKBridgeGetCurrentUserNick();

		[DllImport (OK_IPHONE_DLL)]
		public static extern long OKBridgeGetCurrentUserFBID();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeAuthenticateLocalPlayerWithGameCenter();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeAuthenticateLocalPlayerWithGameCenterAndShowUIIfNecessary();

		[DllImport (OK_IPHONE_DLL)]
		public static extern long OKBridgeGetCurrentUserTwitterID();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeLogoutCurrentUserFromOpenKit();

		[DllImport (OK_IPHONE_DLL)]
		public static extern void OKBridgeGetFacebookFriends(string gameObjectName);

		public OpenKitIOS() {}

		public void SetAppKey(string appKey)
		{
			OKBridgeSetAppKey(appKey);
		}

		public void SetSecretKey(string secretKey)
		{
			OKBridgeSetSecretKey(secretKey);
		}

		public void SetEndpoint(string endpoint)
		{
			OKBridgeSetEndpoint(endpoint);
		}

		public void ShowLeaderboards()
		{
			OKBridgeShowLeaderboards();
		}
		
		public void ShowLeaderboard(int leaderboardID) 
		{
			Debug.Log("ShowLeaderboard(int id) not yet implemented on iOS");
		}

		public void ShowLeaderboardsLandscapeOnly()
		{
			OKBridgeShowLeaderboardsLandscapeOnly();
		}

		public void ShowLoginToOpenKit()
		{
			OKBridgeShowLoginUI();
		}

		public void AuthenticateLocalPlayerToGC()
		{
			OKBridgeAuthenticateLocalPlayerWithGameCenter();
		}

		public void AuthenticateLocalPlayerToGCAndShowUIIfNecessary()
		{
			OKBridgeAuthenticateLocalPlayerWithGameCenterAndShowUIIfNecessary();
		}

		public void SubmitScoreComponent(OKScoreSubmitComponent score)
		{
			if(string.IsNullOrEmpty(score.gameCenterLeaderboardCategory)) {
				Debug.Log("Submitting score to OpenKit");
				OKBridgeSubmitScore(score.scoreValue, score.OKLeaderboardID, score.metadata, score.displayString, score.GetCallbackGameObjectName());
			} else {
				Debug.Log("Submitting score to OpenKit & Gamecenter");
				OKBridgeSubmitScoreWithGameCenter(score.scoreValue, score.OKLeaderboardID, score.metadata, score.displayString, score.GetCallbackGameObjectName(), score.gameCenterLeaderboardCategory);
			}
		}
		public void SubmitAchievementScore(OKAchievementScore achievementScore)
		{
			OpenKit.OKLog.Error("Submit achievement score is not yet implemented on iOS");
		}

		public OKUser GetCurrentUser()
		{
			int okID = OKBridgeGetCurrentUserOKID();
			OKLog.Info("Current openkit user id: " + okID);

			if(okID == 0)
				return null;
			else {
				OKUser user = new OKUser();
				user.OKUserID = okID;
				user.userNick = OKBridgeGetCurrentUserNick();
				user.FBUserID = OKBridgeGetCurrentUserFBID();
				user.twitterUserID = OKBridgeGetCurrentUserTwitterID();
				return user;
			}
		}

		public void LogoutCurrentUserFromOpenKit()
		{
			OKBridgeLogoutCurrentUserFromOpenKit();
		}

		public void GetFacebookFriendsList(OKNativeAsyncCall functionCall)
		{
			OKBridgeGetFacebookFriends(functionCall.GetCallbackGameObjectName());
		}
		
		public void SetAchievementsEnabled(bool enabled)
		{
			Debug.Log("OpenKit achievements not yet implemented on iOS, so SetAchievementsEnabled is also NYI");
		}
		
		public void SetLeaderboardListTag(String tag) 
		{
			OKBridgeSetLeaderboardListTag(tag);
		}
		public void SetGoogleLoginEnabled(bool enabled) 
		{
			Debug.Log("Google auth not supported on iOS yet, so SetGoogleAuthEnabled(boolean) does nothing on iOS");
		}
		
	}
}


