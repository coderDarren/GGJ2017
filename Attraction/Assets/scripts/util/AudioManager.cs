using UnityEngine;
using System.Collections;
using Types;

namespace Util {

	[RequireComponent(typeof(AudioSource))]
	public class AudioManager : MonoBehaviour {

		const string PREF_AUDIO_INITIALIZED = "audioinitialized";

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
		AudioState music;
		AudioState sfx;

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
				ConfigurePrefs();
				ConfigureAudio();
				SetSceneBasedAudio(GameScene.SPLASH);
			}
		}

		void ConfigurePrefs()
		{
			int status = PlayerPrefs.GetInt(PREF_AUDIO_INITIALIZED);
			if (status != 1) {
				PlayerPrefs.SetInt(PREF_AUDIO_INITIALIZED, 1);
				PlayerPrefs.SetInt(Types.AudioType.MUSIC.ToString(), 1);
				PlayerPrefs.SetInt(Types.AudioType.SFX.ToString(), 1);
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

			int status = GetStatus(Types.AudioType.MUSIC);
			if (status != 1)
				music = AudioState.OFF;
			status = GetStatus(Types.AudioType.SFX);
			if (status != 1)
				sfx = AudioState.OFF;
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

			if (toVolume == 0 && music == AudioState.ON) {
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

			if (music == AudioState.OFF)
				return;

			StopCoroutine("FadeAudioVolume");
			StartCoroutine("FadeAudioVolume", 0);
		}

		public void ToggleMusic()
		{
			music = music == AudioState.ON ? AudioState.OFF : AudioState.ON;
			SetStatus(Types.AudioType.MUSIC, music);
			StopCoroutine("FadeAudioVolume");
			StartCoroutine("FadeAudioVolume", 0);
		}

		public void ToggleSFX()
		{
			sfx = sfx == AudioState.ON ? AudioState.OFF : AudioState.ON;
			SetStatus(Types.AudioType.SFX, sfx);
		}

		public int GetStatus(Types.AudioType audio)
		{
			return PlayerPrefs.GetInt(audio.ToString());
		}

		void SetStatus(Types.AudioType audio, AudioState state)
		{
			int status = state == AudioState.ON ? 1 : 0;
			PlayerPrefs.SetInt(audio.ToString(), status);
		}
	}
}