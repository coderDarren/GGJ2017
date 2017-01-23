using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;

public class LevelWinPage : MonoBehaviour {

	public Image star1, star2, star3;
	public Text storyText;

	int totalStars;

	public void ConfigurePage(GalaxyType galaxy, int level, int stars)
	{
		string story = StoryManager.Instance.GetStory(galaxy, level);
		if (story != null) {
			storyText.text = story;
		}
		totalStars = stars;
		StartCoroutine("WaitToFill");

		if (level < 5)
		{
			int status = ProgressManager.Instance.GetStatus(galaxy, level + 1);
			if (status == -1) //the level is locked
			{
				ProgressManager.Instance.SetProgress(galaxy, level + 1, 0); //unlock it
			}
		}
		else
		{
			GalaxyType nextGalaxy = (GalaxyType)((int)galaxy + 1);
			int status = ProgressManager.Instance.GetStatus(nextGalaxy, 0);
			if (status == -1) //galaxy is locked
				ProgressManager.Instance.SetProgress(nextGalaxy, 0, 0); //unlock it
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
