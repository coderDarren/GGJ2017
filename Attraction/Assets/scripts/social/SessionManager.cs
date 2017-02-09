using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Types;
using Util;
using Menu;

#if UNITY_ANDROID
	using GooglePlayGames;
	using GooglePlayGames.BasicApi;
#endif

public class SessionManager : MonoBehaviour {

	public static SessionManager Instance;

	public delegate void RefreshDelegate();
	public static event RefreshDelegate RefreshSocialUsability;

	public string userName { get; private set; }
	public string userId;
	public bool validUser { 
		get { return Social.localUser.authenticated; }
	}

	SceneLoader scene;

	void Awake() {
		if (Instance == null) {
			scene = SceneLoader.Instance;

#if UNITY_ANDROID
			ConfigureGooglePlay();
#elif !UNITY_ANDROID
			StartCoroutine("WaitToStart");
#endif

			Instance = this;

		}
	}

	IEnumerator WaitToStart() {
		yield return new WaitForSeconds(2);
		scene.LoadScene(GameScene.MENU);
	}

	void ConfigureGooglePlay() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(ProcessAuthentication);
	}

	void ProcessAuthentication(bool success) {
		if (success) {
			userName = Social.localUser.userName;
			userId = Social.localUser.id;
			GetComponent<ProgressManager>().CheckResetProgress();
			GetComponent<TutorialManager>().CheckResetTutorials();
		} else {
			userId = string.Empty;
		}
		StartCoroutine("WaitToStart");
	}

	IEnumerator Logout() {
		PlayGamesPlatform.Instance.SignOut();

		while (validUser) {
			yield return null;
		}

		Login();
	}

	void Login() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			RefreshSocialUsability();
			if (success) {
				userName = Social.localUser.userName;
				userId = Social.localUser.id;
				GetComponent<ProgressManager>().CheckResetProgress();
				GetComponent<TutorialManager>().CheckResetTutorials();
			} else {
				userId = string.Empty;
			}
		});
	}

	public void HandleLogin() {

#if UNITY_ANDROID
		StopCoroutine("Logout");
		StartCoroutine("Logout");
#endif

	}

	public void ShowAchievements() {

#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowAchievementsUI();
#endif

	}

	public void ShowLeaderboard() {

#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI(Remnant.GPGSIds.leaderboard_stars_earned);
#endif

	}

	public void IncrementEvent(string eventId, uint amount) {
		if (!validUser) 
			return;

		PlayGamesPlatform.Instance.Events.IncrementEvent(eventId, amount);
	}

	public void IncrementLevelAchievement(GalaxyType galaxy, int level, int amount) {
		if (!validUser)
			return;

		string achievementId = GPGSUtil.GetGalaxyAchievementId(galaxy);
		if (achievementId == string.Empty) {
			return;
		}

		PlayGamesPlatform.Instance.IncrementAchievement(
	 	   achievementId, amount, (bool success) => {
	 	   	if (success) {
	 	   		PageManager.Instance.LoadPage(PageType.ACHIEVEMENT_PROGRESS);
	 	   		AchievementProgressPage page = GameObject.FindObjectOfType<AchievementProgressPage>();
	 	   		page.Configure(level - 1, level, 5);
	 	   	}
	   	});
	}
}
