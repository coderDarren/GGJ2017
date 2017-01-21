using UnityEngine;
using System.Collections;
using Types;

namespace Util {

	[RequireComponent(typeof(AudioSource))]
	public class AudioManager : MonoBehaviour {

		public static AudioManager Instance;

		[System.Serializable]
		public struct AudioInfo
		{
			public AudioClip clip;
			public float volume;
			public GameScene scene;
		}

		public AudioInfo[] audioInfo;
		public float audioFadeInSpeed = 1.5f;
		public float audioFadeOutSpeed = 0.25f;

		AudioSource audio;
		AudioInfo targetInfo;
		Hashtable audioLookupTable;

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
			if (Instance == this) {
				audio = GetComponent<AudioSource>();
				ConfigureAudio();
				SetSceneBasedAudio(GameScene.SPLASH);
			}
		}

		void ConfigureAudio()
		{
			audioLookupTable = new Hashtable();
			for (int i = 0; i < audioInfo.Length; i++)
			{
				int index = (int)audioInfo[i].scene;
				audioLookupTable[index] = audioInfo[i];
			}
		}

		IEnumerator FadeAudioVolume(float toVolume)
		{
			float fadeSpeed = audioFadeInSpeed;
			if (toVolume == 0)
				fadeSpeed = audioFadeOutSpeed;

			while (Mathf.Abs(toVolume - audio.volume) > 0.01f)
			{
				audio.volume = Mathf.Lerp(audio.volume, toVolume, fadeSpeed * Time.deltaTime);
				yield return null;
			}

			audio.volume = toVolume;

			if (toVolume == 0) {
				audio.Stop();
				audio.clip = targetInfo.clip;
				audio.time = Random.Range(0, 2 * (audio.clip.length / 3));
				audio.Play();
				StartCoroutine("FadeAudioVolume", targetInfo.volume);
			}
		}

		public void SetSceneBasedAudio(GameScene gameScene)
		{
			int index = (int)gameScene;
			targetInfo = (AudioInfo)audioLookupTable[index];

			if (targetInfo.clip == audio.clip)
				return;

			StopCoroutine("FadeAudioVolume");
			StartCoroutine("FadeAudioVolume", 0);
		}
	}
}