using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Types;
using Debug;

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

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				_canvas = GetComponent<Canvas>();
				_canvasGroup = GetComponent<CanvasGroup>();
			}
		}

		/// ----------------------- TRANSITIONS -----------------------

		IEnumerator FadeSceneOut()
		{
			ApplicationLoader.Instance.StartLoading();
			yield return new WaitForSeconds(0.75f);
			SceneManager.LoadScene(_targetScene);
		}

		IEnumerator FadeSceneIn()
		{
			yield return new WaitForSeconds(0.5f);
			ApplicationLoader.Instance.StopLoading();
		}

		void OnLevelWasLoaded(int level)
		{
			Debugger.Log(SceneManager.GetActiveScene().name + " was loaded.", DebugFlag.EVENT);
			StartCoroutine("FadeSceneIn");
		}

		/// ----------------------- PUBLIC FUNCTIONS -----------------------

		/// <summary>
		/// Loads scene based on the _gameScene enumeration that is passed
		/// </summary>
		public void LoadScene(GameScene _gameScene)
		{
			_targetScene = Utility.SceneToString(_gameScene);

			if (_targetScene.Equals("NULL"))
			{
				Debugger.LogWarning("You tried to load a scene that does not exist.");
				return;
			}

			StartCoroutine("FadeSceneOut");
		}
	}

}
