using UnityEngine;
using System.Collections;
using Menu;
using Types;

public class ZoomToGalaxyEvent : ButtonEvent {

	public GalaxyType galaxy;

	LevelSceneController scene;

	void Start()
	{
		scene = GameObject.FindObjectOfType<LevelSceneController>();
		InitButton();
	}

	public override void OnItemUp()
	{
		base.OnItemUp();
		scene.ViewGalaxy(galaxy);
		ProgressManager.Instance.CompleteProgress(galaxy);
	}
}
