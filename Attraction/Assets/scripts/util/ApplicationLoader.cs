﻿using UnityEngine;
using System.Collections;
using Types;

namespace Util {

	/// <summary>
	/// This class provides control over the main application loading cogs & text status
	/// </summary>
	public class ApplicationLoader : MonoBehaviour {

		public static ApplicationLoader Instance;

		public ProgressNotificationEffect loadingEffect1;
		public ProgressNotificationEffect loadingEffect2;

		public bool loading { get; private set; }
		public bool sceneIsFadedOut { get; private set; }

		Canvas _canvas;
		CanvasGroup _canvasGroup;
		float _transitionAlpha = 0;
		public float fadeTransitionSmooth = 0.5f;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				_canvas = GetComponent<Canvas>();
				_canvasGroup = GetComponent<CanvasGroup>();
				_canvasGroup.alpha = _transitionAlpha;
				loadingEffect1.On = false;
				loadingEffect2.On = false;
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
			//_transitionAlpha = 0;

			while (_transitionAlpha < 0.98f)
			{
				_transitionAlpha += fadeTransitionSmooth * Time.deltaTime;
				_canvasGroup.alpha = _transitionAlpha;
				yield return null;
			}
			_canvasGroup.alpha = 1;
			sceneIsFadedOut = true;

			loadingEffect1.On = true;
			loadingEffect2.On = true;
		}

		IEnumerator FadeSceneIn()
		{
			loadingEffect1.On = false;
			loadingEffect2.On = false;

			yield return new WaitForSeconds(1);
			//_transitionAlpha = 1;
			while (_transitionAlpha > 0.02f)
			{
				if (!_canvasGroup)
					_canvasGroup = GetComponent<CanvasGroup>();
				_transitionAlpha -= fadeTransitionSmooth * Time.deltaTime;
				_canvasGroup.alpha = _transitionAlpha;
				yield return null;
			}
			_canvasGroup.alpha = 0;
			sceneIsFadedOut = false;
			//force load screen to be under of all other screens (only after it is fully transparent)
			_canvas.sortingOrder = -10;

		}
	}
}