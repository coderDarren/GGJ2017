using UnityEngine;
using System.Collections;
using Menu;

public class GameStateEvent : ButtonEvent {

	public enum EventType {
		PAUSE,
		RESUME
	}

	public EventType eventType;

	ShipController ship;

	public override void OnItemUp() {
		base.OnItemUp();

		ship = GameObject.FindObjectOfType<ShipController>();

		if (eventType == EventType.PAUSE) {
			PageManager.Instance.LoadPage(PageType.PAUSE);
			ship.Pause();
		} else {
			PageManager.Instance.TurnOffPage(PageType.PAUSE, PageType.NONE);
			ship.Unpause();
		}
	}
}
