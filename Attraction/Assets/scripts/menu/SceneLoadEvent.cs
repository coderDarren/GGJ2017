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

		SceneLoader.Instance.LoadScene(sceneToLoad);	
	}

}

