using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Types;
using DebugServices;

namespace Util {

	/// <summary>
	/// Assists transitioning from one scene to the next smoothly
	/// </summary>
	public class SceneLoader : MonoBehaviour {

		/// ----------------------- PUBLIC MEMBERS -----------------------
		public static SceneLoader Instance;

		/// ----------------------- PRIVATE MEMBERS -----------------------
		Canvas _canvas;
		CanvasGroup _canvasGroup;
		float  _transitionAlpha;
		string _targetScene;
		AudioManager audio;
		bool instant;
		SpriteRenderer enviroFader;

		public GameScene gameScene { get; private set; }

		void Awake()
		{
			if (Instance == null)
			{
				DontDestroyOnLoad(gameObject);
				Instance = this;
				_canvas = GetComponent<Canvas>();
				_canvasGroup = GetComponent<CanvasGroup>();
			}
			else
			{
				Destroy(gameObject);
			}
		}

		void Start()
		{
			if (Instance == this) {
				audio = AudioManager.Instance;
				StartCoroutine("WaitToStart");
			}
		}

		IEnumerator WaitToStart()
		{
			yield return new WaitForSeconds(4);
			LoadScene(GameScene.MENU);
		}

		/// ----------------------- TRANSITIONS -----------------------

		IEnumerator FadeSceneOut()
		{
			ApplicationLoader.Instance.StartLoading();
			while (!ApplicationLoader.Instance.sceneIsFadedOut)
				yield return null;
			SceneManager.LoadScene(_targetScene);
		}

		IEnumerator FadeSceneIn()
		{
			yield return new WaitForSeconds(1);
			ApplicationLoader.Instance.StopLoading();
		}

		void OnLevelWasLoaded(int level)
		{
			Debugger.Log(SceneManager.GetActiveScene().name + " was loaded.", DebugFlag.EVENT);
			audio.SetSceneBasedAudio(gameScene);
			if (gameScene == GameScene.PLAY) {
				LevelLoader.Instance.LoadLevelInfo();
			}
			if (!instant)
				StartCoroutine("FadeSceneIn");
		}

		/// ----------------------- PUBLIC FUNCTIONS -----------------------

		/// <summary>
		/// Loads scene based on the _gameScene enumeration that is passed
		/// </summary>
		public void LoadScene(GameScene _gameScene)
		{
			instant = false;
			_targetScene = Utility.SceneToString(_gameScene);

			if (SceneManager.GetActiveScene().name == "splash")
				ApplicationLoader.Instance.fadeTransitionSmooth = 0.5f;
			else
				ApplicationLoader.Instance.fadeTransitionSmooth = 2.5f;

			if (_targetScene.Equals("NULL"))
			{
				Debugger.LogWarning("You tried to load a scene that does not exist.");
				return;
			}

			gameScene = _gameScene;

			StartCoroutine("FadeSceneOut");
		}

		public void LoadPlaySceneInstant() 
		{
			instant = true;
			_targetScene = Utility.SceneToString(GameScene.PLAY);
			gameScene = GameScene.PLAY;
			StartCoroutine("FadeLevel");
		}

		IEnumerator FadeLevel() {
			enviroFader = GameObject.FindGameObjectWithTag("EnvironmentFader").GetComponent<SpriteRenderer>();
			float target = 1;
			Color curr = enviroFader.color;

			while (curr.a < 0.99f) {
				curr.a = Mathf.Lerp(curr.a, target, 2 * Time.deltaTime);
				enviroFader.color = curr;
				yield return null;
			}

			curr.a = target;
			enviroFader.color = curr;

			SceneManager.LoadScene(_targetScene);
		}
	}

}
