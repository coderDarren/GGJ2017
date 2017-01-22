using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;
using Types;

public class GalaxyNode : MonoBehaviour {

	public GalaxyType galaxy;
	public Sprite padLockIcon;
	public Image circle, icon;
	public Text title;
	public ButtonEvent button;
	public Color disabledColor;

	ProgressManager progress;
	int status;

	void Start()
	{
		progress = ProgressManager.Instance;
		status = progress.GetStatus(galaxy, 0);
		ParseStatus();
	}

	void ParseStatus()
	{
		switch (status)
		{
			case -1: //if the galaxy is not unlocked yet
				icon.color = disabledColor;
				icon.sprite = padLockIcon;
				circle.color = disabledColor;
				title.text = "undiscovered";
				button.enabled = false;
				break;
			case 0: //if the galaxy is new and unopened
			case 1: //if the galaxy is unlocked and already opened
				//defaults (nothing to change)
				break;
		}
	}
}
