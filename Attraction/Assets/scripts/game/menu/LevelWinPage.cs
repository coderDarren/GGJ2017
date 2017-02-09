using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;

public class LevelWinPage : MonoBehaviour {

	public Image star1, star2, star3;
	public Text storyText;

	int totalStars;

	public void ConfigurePage(GalaxyType galaxy, int level, int lives)
	{
		int status = ProgressManager.Instance.GetStatus(galaxy, level);
		int currStars = 0;
		switch (status)
		{
			case 2: currStars = 1; break;
			case 3: currStars = 2; break;
			case 4: currStars = 3; break;
			default: currStars = 0; break;
		}

		int sessionStars = lives >= 3 ? 3 : lives;

		HandleGooglePlayEvents(galaxy, level, sessionStars);

		if (sessionStars > currStars) {
			ProgressManager.Instance.SetProgress(galaxy, level, sessionStars + 1);
		}

		string story = StoryManager.Instance.GetStory(galaxy, level);
		if (story != null) {
			storyText.text = story;
		}
		totalStars = sessionStars;
		StartCoroutine("WaitToFill");

		if (level < 5)
		{
			status = ProgressManager.Instance.GetStatus(galaxy, level + 1);
			if (status == -1) //the level is locked
			{
				ProgressManager.Instance.SetProgress(galaxy, level + 1, 0); //unlock it
			}
		}
		else
		{
			LevelLoader.Instance.ResetTargetGalaxy();
			GalaxyType nextGalaxy = (GalaxyType)((int)galaxy + 1);
			status = ProgressManager.Instance.GetStatus(nextGalaxy, 0);
			if (status == -1) //galaxy is locked
				ProgressManager.Instance.SetProgress(nextGalaxy, 0, 0); //unlock it
		}
	}

	void HandleGooglePlayEvents(GalaxyType galaxy, int level, int stars) {

		int status = ProgressManager.Instance.GetStatus(galaxy, level);

		//add new stars earned
		if (stars - status >= 0) {
			uint unlockedStars = (uint)(stars - status) + 1;
			SessionManager.Instance.IncrementEvent(Remnant.GPGSIds.event_stars_earned, unlockedStars);
		}

		//increment galaxy completion achievement
		if (status <= 1) { //means before this win, level had not been beaten
			SessionManager.Instance.IncrementLevelAchievement(galaxy, level, 1);
		}
	}

	IEnumerator WaitToFill()
	{
		yield return new WaitForSeconds(1);
		StartCoroutine(FillStar(star1, 1));
	}

	IEnumerator FillStar(Image img, int star)
	{
		while (img.fillAmount < 0.98f)
		{
			img.fillAmount += 1 * Time.deltaTime;
			yield return null;
		}

		img.fillAmount = 1;

		if (star < totalStars) {
			star += 1;
			if (star == 2)
				StartCoroutine(FillStar(star2, 2));
			if (star == 3)
				StartCoroutine(FillStar(star3, 3));
		}
	}
}
