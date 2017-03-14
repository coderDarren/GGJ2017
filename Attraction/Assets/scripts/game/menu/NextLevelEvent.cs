using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;
using Types;
using Util;

public class NextLevelEvent : ButtonEvent {

	public enum EventType {
		NEXT,
		PREVIOUS
	}

	public EventType eventType;
	public Text lvlText;
	public Color inactiveColor;
	public PageType pageToRemove;
	public PageType[] additionalPagesToRemove;
	public bool initOnStart;

	LevelLoader level;
	ProgressManager progress;
	int currLevel;
	int targetLevel;
	GalaxyType targetGalaxy;

	void Start() {
		InitButton();
		progress = ProgressManager.Instance;
		level = LevelLoader.Instance;
		if (initOnStart)
			ConfigureButton();
	}

	//probably called by level win or lose page
	public void ConfigureButton() {

		targetGalaxy = level.targetGalaxy;
		currLevel = level.targetInfo.level;
		bool targetIsAvailable = true;

		if (currLevel > 1 && currLevel < 5) {

			targetLevel = (eventType == EventType.NEXT) ? currLevel + 1 : currLevel - 1;
			targetIsAvailable = progress.LevelIsAvailable(targetGalaxy, targetLevel);

		} else if (currLevel == 1) {

			if (eventType == EventType.NEXT) {
				targetLevel = currLevel + 1;
				targetIsAvailable = progress.LevelIsAvailable(targetGalaxy, targetLevel);
			} else {
				targetGalaxy = progress.PreviousGalaxy(level.targetGalaxy);
				targetLevel = 5;
				targetIsAvailable = progress.LevelIsAvailable(targetGalaxy, targetLevel);
			}

		} else if (currLevel == 5) {

			if (eventType == EventType.NEXT) {
				targetGalaxy = progress.NextGalaxy(level.targetGalaxy);
				targetLevel = 1;
				targetIsAvailable = progress.LevelIsAvailable(targetGalaxy, targetLevel);
			} else {
				targetLevel = 4;
			}
		}

		if (targetGalaxy == GalaxyType.NONE) targetIsAvailable = false; //in this context, galaxy type of NONE would indicate the galaxy does not exist

		int adjustedLevel = targetGalaxy == GalaxyType.HOME_GALAXY ? targetLevel :
							targetGalaxy == GalaxyType.DAHKRI_GALAXY ? targetLevel + 5 :
							targetGalaxy == GalaxyType.XILYANTIPHOR_GALAXY ? targetLevel + 10 :
							targetGalaxy == GalaxyType.ZAX_GALAXY ? targetLevel + 15 :
							targetGalaxy == GalaxyType.VIDON_GALAXY ? targetLevel + 20 :
							targetGalaxy == GalaxyType.KYDOR_GALAXY ? targetLevel + 25 :
							targetGalaxy == GalaxyType.RYKTAR_GALAXY ? targetLevel + 30 :
							targetGalaxy == GalaxyType.MALIX_GALAXY ? targetLevel + 35 : targetLevel;

		lvlText.text = "LVL " +adjustedLevel.ToString();

		if (!targetIsAvailable) {
			interactable = false;
			_image.color = inactiveColor;
			lvlText.color = inactiveColor;

			if (targetGalaxy == GalaxyType.NONE) {
				//gameObject.SetActive(false);
				lvlText.text = "";
			}
		}
	}

	public override void OnItemUp() {
		base.OnItemUp();

		level.SetLevelInfo(targetGalaxy, targetLevel);
		if (pageToRemove != PageType.NONE)
			{
				for (int i = 0; i < additionalPagesToRemove.Length; i++) {
					PageManager.Instance.TurnOffPage(additionalPagesToRemove[i], PageType.NONE);
				}
				PageManager.Instance.TurnOffPage(pageToRemove, PageType.NONE);
			}
		progress.MarkLevelAttempted(targetGalaxy, targetLevel);
		SceneLoader.Instance.LoadPlaySceneInstant();
	}
}
