using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;
using Menu;

public class LevelNode : MonoBehaviour {

	public GalaxyType galaxy;
	public int level;
	public Sprite filledStar;
	public Image star1, star2, star3;
	public Image circle, padLock;
	public Text levelNum;
	public ButtonEvent button;
	public Color disabledColor, enabledColor;

	ProgressManager progress;
	bool available;
	int stars;

	void Start()
	{
		progress = ProgressManager.Instance;
		available = progress.LevelIsAvailable(galaxy, level);
		stars = progress.GetLevelStars(galaxy, level);
		HandleStatus();
	}

	void HandleStatus()
	{
		if (!available) {
			star1.enabled = false;
			star2.enabled = false;
			star3.enabled = false;
			levelNum.enabled = false;
			padLock.color = disabledColor;
			circle.color = disabledColor;
			button.enabled = false;
			return;
		}

		switch (stars) {
			case 0:
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 1:
				star1.sprite = filledStar;
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 2:
				star1.sprite = filledStar;
				star2.sprite = filledStar;
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 3:
				star1.sprite = filledStar;
				star2.sprite = filledStar;
				star3.sprite = filledStar;
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
		}

	}
}
