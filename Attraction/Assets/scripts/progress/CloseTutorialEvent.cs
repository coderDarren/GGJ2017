using UnityEngine;
using System.Collections;
using Menu;
using Types;
public class CloseTutorialEvent : ButtonEvent {

	public TutorialType tutorial;
	public PageType[] pagesToClose;

	public override void OnItemUp()
	{
		TutorialManager.Instance.CompleteTutorial(tutorial);
		for (int i = 0; i < pagesToClose.Length; i++) {
			PageManager.Instance.TurnOffPage(pagesToClose[i], PageType.NONE);
		}
	}
}
