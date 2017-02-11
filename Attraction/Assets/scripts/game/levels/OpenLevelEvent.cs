using UnityEngine;
using System.Collections;
using Menu;
using Types;
using Util;

public class OpenLevelEvent : ButtonEvent {

	public GalaxyType galaxy;
	public int level;

	public override void OnItemUp()
	{
		base.OnItemUp();
		LevelLoader.Instance.SetLevelInfo(galaxy, level);
		ProgressManager.Instance.MarkLevelAttempted(galaxy, level);
		SceneLoader.Instance.LoadScene(GameScene.PLAY);
	}
}
