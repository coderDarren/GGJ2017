using UnityEngine;
using System.Collections;

namespace Util {

	/// <summary>
	/// This class provides control over the main application loading cogs & text status
	/// </summary>
	public class ApplicationLoader : MonoBehaviour {

		public static ApplicationLoader Instance;

		public LoadingGears loadingGears;

		public bool loading { get; private set; }

		Canvas _canvas;
		CanvasGroup _canvasGroup;
		float _transitionAlpha = 0;
		float _fadeTransitionSmooth = 5;

		void Start()
		{
			if (Instance == null)
			{
				Instance = this;
				_canvas = GetComponent<Canvas>();
				_canvasGroup = GetComponent<CanvasGroup>();
				_canvasGroup.alpha = _transitionAlpha;
			}
		}

		public void StartLoading()
		{
			loading = true;
			StopAllCoroutines();
			StartCoroutine("FadeSceneOut");
		}

		public void StopLoading()
		{
			loading = false;
			StopAllCoroutines();
			StartCoroutine("FadeSceneIn");
		}

		IEnumerator FadeSceneOut()
		{
			//force load screen to be on top of all other screens
			_canvas.sortingOrder = 1001;
			_transitionAlpha = 0;
			loadingGears.StartLoading();

			while (_transitionAlpha < 0.98f)
			{
				_transitionAlpha += _fadeTransitionSmooth * Time.deltaTime;
				_canvasGroup.alpha = _transitionAlpha;
				yield return new WaitForSeconds(Time.deltaTime);
			}
			_canvasGroup.alpha = 1;
		}

		IEnumerator FadeSceneIn()
		{
			yield return new WaitForSeconds(1);
			_transitionAlpha = 1;
			while (_transitionAlpha > 0.02f)
			{
				if (!_canvasGroup)
					_canvasGroup = GetComponent<CanvasGroup>();
				_transitionAlpha -= _fadeTransitionSmooth * Time.deltaTime;
				_canvasGroup.alpha = _transitionAlpha;
				yield return new WaitForSeconds(Time.deltaTime);
			}
			_canvasGroup.alpha = 0;
			loadingGears.StopLoading();
			//force load screen to be under of all other screens (only after it is fully transparent)
			_canvas.sortingOrder = -10;
		}
	}
}