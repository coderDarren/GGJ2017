using UnityEngine;
using System.Collections;
using Menu;
using Types;
using Util;

public class OpenLevelEvent : ButtonEvent {

	public GalaxyType galaxy;
	public int level;

	int progress;

	public override void OnItemUp()
	{
		base.OnItemUp();
		LevelLoader.Instance.SetLevelInfo(galaxy, level);
		progress = ProgressManager.Instance.GetStatus(galaxy, level);
		if (progress == 0)
			ProgressManager.Instance.CompleteProgress(galaxy, level);
		SceneLoader.Instance.LoadScene(GameScene.PLAY);
	}
}
