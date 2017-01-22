using UnityEngine;
using System.Collections;
using Types;

public class StoryManager : MonoBehaviour {

	public static StoryManager Instance;

	[System.Serializable]
	public class StoryInfo {
		public GalaxyType galaxy;
		public int level;
		public string journalEntry;
	}

	public StoryInfo[] storyInfo;

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

	public string GetStory(GalaxyType galaxy, int level)
	{
		for (int i = 0; i < storyInfo.Length; i++)
		{
			if (galaxy == storyInfo[i].galaxy && level == storyInfo[i].level) {
				return storyInfo[i].journalEntry;
			}
		}

		return null;
	}
}
