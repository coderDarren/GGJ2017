using UnityEngine;
using System.Collections;
using Types;

public class LevelLoader : MonoBehaviour {

	[System.Serializable]
	public struct LeveLInfo {
		public GalaxyType galaxy;
		public int level;
		public GameObject prefab;
	}

	public static LevelLoader Instance;
	public LeveLInfo[] levelInfo;

	GalaxyType galaxy;
	LeveLInfo targetInfo;
	int level;

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

	public void SetLevelInfo(GalaxyType galaxy, int level)
	{
		for (int i = 0; i < levelInfo.Length; i++)
		{
			if (levelInfo[i].galaxy == galaxy && levelInfo[i].level == level) {
				targetInfo = levelInfo[i];
				return;
			}
		}
	}

	public void LoadLevelInfo()
	{
		Instantiate<GameObject>(targetInfo.prefab);
	}
}
