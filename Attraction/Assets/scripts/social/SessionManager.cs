using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Types;
using Util;
using Menu;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;

public class SessionManager : MonoBehaviour {

	public static SessionManager Instance;

	public delegate void RefreshDelegate();
	public static event RefreshDelegate RefreshSocialUsability;

	public string userName { get; private set; }
	public string userId;
	public bool validUser { 
		get { return PlayGamesPlatform.Instance.IsAuthenticated(); }
	}
	public int userStars { get {return stars;} }
	public int userResources { get {return resources;} }

	SceneLoader scene;
	int stars;
	int resources;

	void Start() {
		if (Instance == null) {
			scene = SceneLoader.Instance;

#if UNITY_ANDROID
			ConfigureGooglePlay();
			Social.localUser.Authenticate(ProcessAuthentication);
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
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
																			  .Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}

	void UpdateUser() {
		userName = Social.localUser.userName;
		userId = Social.localUser.id;
		GetComponent<ProgressManager>().CheckResetProgress();
		GetComponent<TutorialManager>().CheckResetTutorials();
		PollStarScore();
		PollResourceScore();
	}

	void ProcessAuthentication(bool success) {
		if (success) {
			UpdateUser();
		} else {
			userId = string.Empty;
		}
		StartCoroutine("WaitToStart");
	}

	IEnumerator Logout() {
		yield return null;
		PlayGamesPlatform.Instance.SignOut();
		while (validUser) {
			yield return null;
		}
		Login();
	}

	void Login() {
		ConfigureGooglePlay();
		Social.localUser.Authenticate((success) => {
			if (success) {
				UpdateUser();
			} else {
				userId = string.Empty;
			}
			RefreshSocialUsability(); 
		});
	}

	public void HandleLogin() {

#if UNITY_ANDROID
		StopCoroutine("Logout");
		StartCoroutine("Logout");
#endif

	}

	public void ShowAchievements() {

		PlayGamesPlatform.Instance.ShowAchievementsUI();
	}

	public void ShowLeaderboard() {

		PlayGamesPlatform.Instance.ShowLeaderboardUI();
	}

	public int GetEventValue(string eventId) {
		if (!validUser) {
			return 0;
		}

		int ret = 0;

		PlayGamesPlatform.Instance.Events.FetchEvent(
			DataSource.ReadCacheOrNetwork,
			eventId,
			(rs, e) => {
				ret = (int)e.CurrentCount;
				Debug.LogError("EVENT "+eventId+" INCREMENTED TO " +ret);
			});
		return ret;
	}

	public void IncrementEvent(string eventId, uint amount) {
		if (!validUser) 
			return;

		int ret = GetEventValue(eventId);
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
	 	   achievementId, amount, (success) => {
	 	   	if (success) {
	 	   		PageManager.Instance.LoadPage(PageType.ACHIEVEMENT_PROGRESS);
	 	   		AchievementProgressPage page = GameObject.FindObjectOfType<AchievementProgressPage>();
	 	   		page.Configure(level - 1, level, 5);
	 	   	}
	   	});
	}

	void PollStarScore() {
		PlayGamesPlatform.Instance.LoadScores(
			Remnant.GPGSIds.leaderboard_stars_earned,
			LeaderboardStart.PlayerCentered,
			1,
			LeaderboardCollection.Public,
			LeaderboardTimeSpan.AllTime,
			(data) => {	
				stars = (int)data.Scores[0].value;
				//for (int i = 0; i < data.Scores.Length; i++) {
				//	Debug.LogError("SCORE DATA => "+data.Scores[i].value);
				//}
			});
	}

	void PollResourceScore() {
		PlayGamesPlatform.Instance.LoadScores(
			Remnant.GPGSIds.leaderboard_resources_earned,
			LeaderboardStart.PlayerCentered,
			1,
			LeaderboardCollection.Public,
			LeaderboardTimeSpan.AllTime,
			(data) => {	
				resources = (int)data.Scores[0].value;
				//for (int i = 0; i < data.Scores.Length; i++) {
				//	Debug.LogError("SCORE DATA => "+data.Scores[i].value);
				//}
			});
	}

	public void ReportStarScore(uint score) {
		if (!validUser) 
			return;

		stars = (int)score;
		Social.ReportScore(score, Remnant.GPGSIds.leaderboard_stars_earned, (bool success) => {});
	}

	public void ReportResourceScore(uint score) {
		if (!validUser)
			return;

		resources = (int)score;
		Social.ReportScore(score, Remnant.GPGSIds.leaderboard_resources_earned, (bool success) => {});
	}
}
