using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;
using Types;

public class GalaxyNode : MonoBehaviour {

	public GalaxyType galaxy;
	public Sprite padLockIcon;
	public Image icon;
	public Text title;
	public ButtonEvent button;
	public Color disabledColor;

	ProgressManager progress;

	void Start()
	{
		progress = ProgressManager.Instance;
		HandleAvailability();
	}

	void HandleAvailability()
	{
		if (!progress.GalaxyIsAvailable(galaxy)) {
			icon.color = disabledColor;
			icon.sprite = padLockIcon;
			if (title != null) {
				title.text = "undiscovered";
				title.color = disabledColor;
			}
			button.enabled = false;
		}
	}
}
