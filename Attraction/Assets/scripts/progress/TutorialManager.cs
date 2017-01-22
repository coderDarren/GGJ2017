using UnityEngine;
using System.Collections;
using System;
using Types;
using Menu;

public class TutorialManager : MonoBehaviour {

	public static TutorialManager Instance;

	const string INITIALIZED = "TUTORIALS_INITIALIZED";

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

	void Start()
	{
		int init = GetStatus(INITIALIZED);
		if (init != 1)
			InitializeTutorials();
	}

	void InitializeTutorials()
	{
		Array tutorials = Enum.GetValues(typeof(TutorialType));
		foreach (TutorialType tutorial in tutorials)
		{
			PlayerPrefs.SetInt(tutorial.ToString(), 0);
		}
		PlayerPrefs.SetInt(INITIALIZED, 1);
	}

	public void ResetTutorials()
	{
		InitializeTutorials();
	}

	public int GetStatus(string pref)
	{
		return PlayerPrefs.GetInt(pref);
	}

	public int GetStatus(TutorialType tutorial)
	{
		return PlayerPrefs.GetInt(tutorial.ToString());
	}

	public void StartTutorial(TutorialType tutorial)
	{
		int status = GetStatus(tutorial);
		if (status == 1)
			return;

		switch (tutorial)
		{
			case TutorialType.GALAXIES: PageManager.Instance.LoadPage(PageType.TUTORIALS_GALAXIES); break;
			case TutorialType.HOW_TO_PLAY: PageManager.Instance.LoadPage(PageType.TUTORIALS_HOW_TO_PLAY); break;
		}
	}

	public void CompleteTutorial(TutorialType tutorial)
	{
		PlayerPrefs.SetInt(tutorial.ToString(), 1);
	}
}
