using UnityEngine;
using System.Collections;
using System;
using Types;
using Menu;

public class TutorialManager : MonoBehaviour {

	public static TutorialManager Instance;

	const string INITIALIZED = "INITIALIZED";

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
		}
	}

	public void CompleteTutorial(TutorialType tutorial)
	{
		PlayerPrefs.SetInt(tutorial.ToString(), 1);
	}
}
