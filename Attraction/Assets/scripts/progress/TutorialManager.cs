using UnityEngine;
using System.Collections;
using System;
using Types;
using Menu;

public class TutorialManager : MonoBehaviour {

	public static TutorialManager Instance;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public bool TutorialIsComplete(TutorialType tutorial)
	{
		string dataId = GPGSUtil.TutorialId(tutorial);
		int info = DataStorage.GetLocalData(dataId);
		if (info == 1) return true;
		return false;
	}

	public void TryLaunchTutorial(TutorialType tutorial)
	{
		if (TutorialIsComplete(tutorial))
			return;

		switch (tutorial)
		{
			case TutorialType.BUY_SHIP: PageManager.Instance.LoadPage(PageType.TUTORIALS_BUY_SHIP); break;
			case TutorialType.SHIP_ARMOR: PageManager.Instance.LoadPage(PageType.TUTORIALS_SHIP_ARMOR); break;
			case TutorialType.THRUST_SHIP: PageManager.Instance.LoadPage(PageType.TUTORIALS_THRUST_SHIP); break;
			case TutorialType.COLLECT_RESOURCES: PageManager.Instance.LoadPage(PageType.TUTORIALS_COLLECT_RESOURCES); break;
		}
	}

	public void MarkTutorialComplete(TutorialType tutorial)
	{
		if (TutorialIsComplete(tutorial)) return;

		string dataId = GPGSUtil.TutorialId(tutorial);
		int info = DataStorage.GetLocalData(dataId);
		DataStorage.IncrementEvent(dataId, 1);
	}
}
