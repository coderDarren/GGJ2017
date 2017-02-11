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
		int sessionStars = lives >= 3 ? 3 : lives;
		int currStars = ProgressManager.Instance.GetLevelStars(galaxy, level);

		//add new stars earned
		int unlockedStars = sessionStars - currStars;
		if (unlockedStars > 0) {
			ProgressManager.Instance.MarkLevelStars(galaxy, level, unlockedStars); //adds to level star count
			ProgressManager.Instance.AddStars((uint)unlockedStars); //adds to total star count
		}

		//increment galaxy completion achievement
		if (currStars == 0) { //means before this win, level was not won
			ProgressManager.Instance.MarkLevelAchievement(galaxy, level);
		}

		ProgressManager.Instance.MarkLevelWins(galaxy, level);
		
		string story = StoryManager.Instance.GetStory(galaxy, level);
		if (story != null) {
			storyText.text = story;
		}

		totalStars = sessionStars;
		StartCoroutine("WaitToFill");

		if (level == 5)
		{
			LevelLoader.Instance.ResetTargetGalaxy();
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
			img.fillAmount += 3 * Time.deltaTime;
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
