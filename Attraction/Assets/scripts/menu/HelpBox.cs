using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Util;
using DebugServices;

namespace Menu {

	public class HelpBox : ButtonEvent {

		public delegate void HelpBoxDelegate(HelpBox _helpBox);
		public static event HelpBoxDelegate Open;

		// ------------------- PUBLIC MEMBERS -------------------
		public enum Pivot
		{
			TOP_LEFT,
			MIDDLE_LEFT,
			BOTTOM_LEFT,
			TOP_CENTER,
			MIDDLE_CENTER,
			BOTTOM_CENTER,
			TOP_RIGHT,
			MIDDLE_RIGHT,
			BOTTOM_RIGHT
		}

		public enum OpenMode 
		{
			INSTANT,
			FADE,
			WIDTH_TO_HEIGHT,
			HEIGHT_TO_WIDTH,
			WIDTH_AND_HEIGHT,
		}

		public RectTransform helpBoxRect;

		[System.Serializable]
		public class BoxOptions
		{
			public Pivot    pivot;
			public OpenMode openMode;
			public OpenMode closeMode;
			public Vector2  targetSize;
			public Vector2  resizeSmooth;
			public float 	fadeSmooth;
			public float 	lifeSpan;
		}

		public BoxOptions options;
		public Text  titleText;
		public Text  descriptionText;
		public Image icon;

		// ------------------- PRIVATE MEMBERS -------------------

		CanvasGroup _helpBoxCanvas;

		Vector2 _targetSize;
		Vector2 _currSize;
		Vector2 _resizeVel;

		float _fadeVel;
		float _lifeTimer;
		bool _isOpen;

		// ------------------- UNITY EVENT FUNCTIONS -------------------

		void Start()
		{
			Init();
		}

		void OnDisable()
		{
			HelpBox.Open -= OpenHelpBox;
		}

		// ------------------- PUBLIC FUNCTIONS -------------------

		// ------------------- INHERITED OVERRIDES -------------------

		public override void OnItemUp()
		{
			base.OnItemUp();
			helpBoxRect.pivot = GetPivot(); //updating pivot for editor runtime changes
			Open(this); //event will be sent to all other help boxes that are open
		}

		// ------------------- PRIVATE FUNCTIONS -------------------

		/// <summary>
		/// Called via event from the help box that was last opened.
		/// If this helpbox is not the helpbox that was last opened, this helpbox will be closed
		/// </summary>
		void OpenHelpBox(HelpBox _helpBox)
		{
			if (_helpBox != this) 
			{
				if (_isOpen)
				{
					CloseHelpBox(); //close if it is open and not this
				}
				return;
			}
			else {
				if (_isOpen)
				{
					return; //do nothing if it is open and it is this (do not re-open)
				}
			}

			StopAllCoroutines();

			_isOpen = true;
			_lifeTimer = 0;
			helpBoxRect.gameObject.SetActive(true);

			switch (options.openMode)
			{
				case OpenMode.INSTANT:		    GoInstant(true); 						  break;
				case OpenMode.FADE: 		    StartCoroutine("GoFade", true); 		  break;
				case OpenMode.WIDTH_TO_HEIGHT:  StartCoroutine("GoWidthToHeight", true);  break;
				case OpenMode.HEIGHT_TO_WIDTH:  StartCoroutine("GoHeightToWidth", true);  break;
				case OpenMode.WIDTH_AND_HEIGHT: StartCoroutine("GoWidthAndHeight", true); break;
			}
		}

		/// <summary>
		/// This method public so escape button can close the window
		/// </summary>
		public void CloseHelpBox()
		{
			StopAllCoroutines();

			_isOpen = false;

			switch (options.closeMode)
			{
				case OpenMode.INSTANT: 			GoInstant(false); 						   break;
				case OpenMode.FADE: 			StartCoroutine("GoFade", false); 		   break;
				case OpenMode.WIDTH_TO_HEIGHT: 	StartCoroutine("GoWidthToHeight", false);  break;
				case OpenMode.HEIGHT_TO_WIDTH: 	StartCoroutine("GoHeightToWidth", false);  break;
				case OpenMode.WIDTH_AND_HEIGHT: StartCoroutine("GoWidthAndHeight", false); break;
			}
		}

		void GoInstant(bool _open)
		{
			UpdateTargetSize(_open);
			Utility.SetRectSize(ref helpBoxRect, _targetSize);

			if (_open)
			{
				StartCoroutine("RunLifeClock");
			}
		}

		IEnumerator GoFade(bool _open)
		{
			_currSize = options.targetSize;
			Utility.SetRectSize(ref helpBoxRect, _currSize);

			float _targetAlpha = _open ? 1 : 0;
			_helpBoxCanvas.alpha = _targetAlpha == 1 ? 0 : 1;

			while (Mathf.Abs(_helpBoxCanvas.alpha - _targetAlpha) > 0.02f)
			{
				_helpBoxCanvas.alpha = Mathf.SmoothDamp(_helpBoxCanvas.alpha, _targetAlpha, ref _fadeVel, options.fadeSmooth);
				yield return null;
			}	

			_helpBoxCanvas.alpha = _targetAlpha;

			if (_open)
			{
				StartCoroutine("RunLifeClock");		
			}
			else {
				helpBoxRect.gameObject.SetActive(false);
			}
		}

		IEnumerator GoWidthToHeight(bool _open)
		{
			UpdateTargetSize(_open);
			Utility.SetRectSize(ref helpBoxRect, _currSize);

			if (_open)
			{
				_helpBoxCanvas.alpha = 1;
			}

			//width first
			while (Mathf.Abs(_currSize.x - _targetSize.x) > 0.02f)
			{
				_currSize.x = Mathf.SmoothDamp(_currSize.x, _targetSize.x, ref _resizeVel.x, options.resizeSmooth.x);
				Utility.SetRectWidth(ref helpBoxRect, _currSize.x);
				yield return null;
			}

			//then height
			while (Mathf.Abs(_currSize.y - _targetSize.y) > 0.02f)
			{
				_currSize.y = Mathf.SmoothDamp(_currSize.y, _targetSize.y, ref _resizeVel.y, options.resizeSmooth.y);
				Utility.SetRectHeight(ref helpBoxRect, _currSize.y);
				yield return null;
			}

			if (_open)
			{
				StartCoroutine("RunLifeClock");
			}
			else {
				helpBoxRect.gameObject.SetActive(false);
			}
		}

		IEnumerator GoHeightToWidth(bool _open)
		{
			UpdateTargetSize(_open);
			Utility.SetRectSize(ref helpBoxRect, _currSize);

			if (_open)
			{
				_helpBoxCanvas.alpha = 1;
			}

			//height first
			while (Mathf.Abs(_currSize.y - _targetSize.y) > 0.02f)
			{
				_currSize.y = Mathf.SmoothDamp(_currSize.y, _targetSize.y, ref _resizeVel.y, options.resizeSmooth.y);
				Utility.SetRectHeight(ref helpBoxRect, _currSize.y);
				yield return null;
			}

			//then width
			while (Mathf.Abs(_currSize.x - _targetSize.x) > 0.02f)
			{
				_currSize.x = Mathf.SmoothDamp(_currSize.x, _targetSize.x, ref _resizeVel.x, options.resizeSmooth.x);
				Utility.SetRectWidth(ref helpBoxRect, _currSize.x);
				yield return null;
			}

			if (_open)
			{
				StartCoroutine("RunLifeClock");
			}
			else {
				helpBoxRect.gameObject.SetActive(false);
			}
		}

		IEnumerator GoWidthAndHeight(bool _open)
		{
			UpdateTargetSize(_open);
			Utility.SetRectSize(ref helpBoxRect, _currSize);

			if (_open)
			{
				_helpBoxCanvas.alpha = 1;
			}

			while (Mathf.Abs(_currSize.x - _targetSize.x) > 0.02f &&
				   Mathf.Abs(_currSize.y - _targetSize.y) > 0.02f)
			{
				_currSize.x = Mathf.SmoothDamp(_currSize.x, _targetSize.x, ref _resizeVel.x, options.resizeSmooth.x);
				_currSize.y = Mathf.SmoothDamp(_currSize.y, _targetSize.y, ref _resizeVel.y, options.resizeSmooth.y);
				Utility.SetRectSize(ref helpBoxRect, _currSize);
				yield return null;
			}

			if (_open)
			{
				StartCoroutine("RunLifeClock");
			}
			else {
				helpBoxRect.gameObject.SetActive(false);
			}
		}

		IEnumerator RunLifeClock()
		{
			while (_lifeTimer < options.lifeSpan)
			{
				_lifeTimer += Time.deltaTime;
				yield return new WaitForSeconds(Time.deltaTime);
			}

			_lifeTimer = 0;
			CloseHelpBox();
		}

		void UpdateTargetSize(bool _open)
		{
			if (_open)
			{
				_targetSize = options.targetSize;
				_currSize = Vector2.one * 1;
			}
			else {
				_targetSize = Vector2.one * 1;
			}
		}

		Vector2 GetPivot()
		{
			Vector2 ret = Vector2.zero;

			switch (options.pivot)
			{
				case Pivot.TOP_LEFT: 	  ret = new Vector2(0, 1);       break;
				case Pivot.MIDDLE_LEFT:   ret = new Vector2(0, 0.5f);    break;
				case Pivot.BOTTOM_LEFT:   ret = new Vector2(0, 0);       break;
				case Pivot.TOP_CENTER:    ret = new Vector2(0.5f, 1);    break;
				case Pivot.MIDDLE_CENTER: ret = new Vector2(0.5f, 0.5f); break;
				case Pivot.BOTTOM_CENTER: ret = new Vector2(0.5f, 0);    break;
				case Pivot.TOP_RIGHT: 	  ret = new Vector2(1, 1);       break;
				case Pivot.MIDDLE_RIGHT:  ret = new Vector2(1, 0.5f);    break;
				case Pivot.BOTTOM_RIGHT:  ret = new Vector2(1, 0); 		 break;
			}

			return ret;
		}

		void SubscribeEvents()
		{
			HelpBox.Open += OpenHelpBox;
		}

		// ------------------- PROTECTED FUNCTIONS -------------------

		protected void Init()
		{
			InitButton();
			SubscribeEvents();
			Utility.SetRectSize(ref helpBoxRect, Vector2.zero);
			helpBoxRect.pivot = GetPivot();
			_helpBoxCanvas = helpBoxRect.GetComponent<CanvasGroup>();
		}

		protected void SetTitle(string _t)
		{
			if (!titleText){
				Debugger.LogWarning("You are attempting to set the title of a helpbox, but no text was found.");
				return;
			}

			titleText.text = _t;
		}

		protected void SetDescription(string _d)
		{
			if (!descriptionText){
				Debugger.LogWarning("You are attempting to set the description of a helpbox, but no text was found.");
				return;
			}

			descriptionText.text = _d;
		}

		protected void SetIcon(Sprite _sprite)
		{
			if (!icon){
				Debugger.LogWarning("You are attempting to set the icon of a helpbox, but no icon was found.");
				return;
			}

			icon.sprite = _sprite;
		}

		protected void SetIconColor(Color _color)
		{
			if (!icon){
				Debugger.LogWarning("You are attempting to set the icon color of a helpbox, but no icon color was found.");
				return;
			}

			icon.color = _color;
		}

		// ------------------- INHERITED OVERRIDES -------------------

		public override void OnItemDown()
		{
			if (_isOpen)
			{
				return;
			}
			base.OnItemDown();
		}

		public override void OnItemEnter()
		{
			if (_isOpen)
			{
				return;
			}
			base.OnItemEnter();
		}

		public override void OnItemExit()
		{
			if (_isOpen)
			{
				return;
			}
			base.OnItemExit();
		}
		
	}

}
