using UnityEngine;
using System.Collections;
using Menu;

public class ClearDataEvent : ButtonEvent {

	public enum DataOption {
		PROGRESS,
		TUTORIAL
	}
	public DataOption option;

	public override void OnItemUp()
	{
		base.OnItemUp();

		if (option == DataOption.PROGRESS) {
			ProgressManager.Instance.ResetProgress();
			PageManager.Instance.TurnOffPage(PageType.CLEAR_PROGRESS, PageType.SETTINGS);
		} else {
			TutorialManager.Instance.ResetTutorials();
			PageManager.Instance.TurnOffPage(PageType.CLEAR_TUTORIALS, PageType.SETTINGS);
		}
	}
}
