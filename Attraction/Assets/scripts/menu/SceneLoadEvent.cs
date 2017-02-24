using UnityEngine;
using System.Collections;
using Menu;
using Types;
using Util;

public class SceneLoadEvent : ButtonEvent {

	public GameScene sceneToLoad;

	/// <summary>
	/// Send target scene to the scene controller
	/// </summary>
	public override void OnItemUp()
	{
		base.OnItemUp();

		if (sceneToLoad == GameScene.LEVELS) {
			if (!TutorialManager.Instance.TutorialIsComplete(TutorialType.BUY_SHIP) ||
				!TutorialManager.Instance.TutorialIsComplete(TutorialType.SHIP_ARMOR)) return;
		}

		SceneLoader.Instance.LoadScene(sceneToLoad);	
	}

}

