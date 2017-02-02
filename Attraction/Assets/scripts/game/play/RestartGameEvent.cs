using UnityEngine;
using System.Collections;
using Menu;
using Util;
using Types;

public class RestartGameEvent : ButtonEvent {

	public PageType pageToRemove;

	public override void OnItemUp() {
		base.OnItemUp();
		PageManager.Instance.TurnOffPage(pageToRemove, PageType.NONE);
		GameObject.FindObjectOfType<ShipController>().GetComponent<SpriteRenderer>().sortingOrder = 0;
		SceneLoader.Instance.LoadPlaySceneInstant();
	}
}
