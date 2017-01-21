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
	int status;

	void Start()
	{
		progress = ProgressManager.Instance;
		status = progress.GetStatus(galaxy, level);
		ParseStatus();
	}

	void ParseStatus()
	{
		switch (status)
		{
			case -1:
				star1.enabled = false;
				star2.enabled = false;
				star3.enabled = false;
				levelNum.enabled = false;
				padLock.color = disabledColor;
				circle.color = disabledColor;
				button.enabled = false;
				break;
			case 0: //we use this status to add the ripple effect to newly unlocked levels
			case 1: //if this status is active, it means the player has selected this level, but no stars earned
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 2:
				star1.sprite = filledStar;
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 3:
				star1.sprite = filledStar;
				star2.sprite = filledStar;
				padLock.enabled = false;
				padLock.color = disabledColor;
				circle.color = enabledColor;
				break;
			case 4:
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
