using UnityEngine;
using System.Collections;
using Menu;
using Types;
using Util;

public class CloseTutorialEvent : ButtonEvent {

	public TutorialType tutorial;
	public bool closePage;
	public PageType[] pagesToClose;
	public PageType pageToOpen;
	public bool loadScene;
	public GameScene sceneToLoad;

	public override void OnItemUp()
	{
		if (tutorial == TutorialType.COLLECT_RESOURCES) {
			GameObject.FindObjectOfType<ShipController>().Unpause();
		}

		TutorialManager.Instance.MarkTutorialComplete(tutorial);
		if (loadScene)
			SceneLoader.Instance.LoadScene(sceneToLoad);
		else if (closePage) {
			for (int i = 0; i < pagesToClose.Length - 1; i++) {
				PageManager.Instance.TurnOffPage(pagesToClose[i], PageType.NONE);
			}
			PageManager.Instance.TurnOffPage(pagesToClose[pagesToClose.Length - 1], pageToOpen);
		}
	}
}
