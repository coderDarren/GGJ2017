using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;
using Util;

public class LevelWinPage : MonoBehaviour {

	public Image star1, star2, star3;
	public Text galaxyName;
	public Text lvlText;
	public NextLevelEvent nextEvent;
	public NextLevelEvent prevEvent;

	int totalStars;

	public void ConfigurePage(GalaxyType galaxy, int level, int lives)
	{
		galaxyName.text = Utility.GalaxyToString(galaxy);

		int adjustedLevel = galaxy == GalaxyType.HOME_GALAXY ? level :
							galaxy == GalaxyType.DAHKRI_GALAXY ? level + 5 :
							galaxy == GalaxyType.XILYANTIPHOR_GALAXY ? level + 10 :
							galaxy == GalaxyType.ZAX_GALAXY ? level + 15 :
							galaxy == GalaxyType.VIDON_GALAXY ? level + 20 :
							galaxy == GalaxyType.KYDOR_GALAXY ? level + 25 :
							galaxy == GalaxyType.RYKTAR_GALAXY ? level + 30 :
							galaxy == GalaxyType.MALIX_GALAXY ? level + 35 : level;

		lvlText.text = "LVL " +adjustedLevel.ToString();

		int sessionStars = lives >= 3 ? 3 : lives;
		int currStars = ProgressManager.Instance.GetLevelStars(galaxy, level);

		//add new stars earned
		int unlockedStars = sessionStars - currStars;
		if (unlockedStars > 0) {
			ProgressManager.Instance.MarkLevelStars(galaxy, level, unlockedStars); //adds to level star count
			ProgressManager.Instance.AddStars((uint)unlockedStars); //adds to total star count
			ProgressManager.Instance.AddResources((uint)unlockedStars * 50);
		}

		//increment galaxy completion achievement
		if (currStars == 0) { //means before this win, level was not won
			ProgressManager.Instance.MarkLevelAchievement(galaxy, level);
		}

		//increment 3 star gain achievement
		if (sessionStars == 3 && currStars < 3) {
			ProgressManager.Instance.MarkThreeStarAchievement();
		}

		ProgressManager.Instance.MarkLevelWins(galaxy, level);

		totalStars = sessionStars;
		StartCoroutine("WaitToFill");

		nextEvent.ConfigureButton();
		prevEvent.ConfigureButton();

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
