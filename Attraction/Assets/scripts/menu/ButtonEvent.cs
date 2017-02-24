using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DebugServices;
using Util;
using Types;

namespace Menu {

	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(RectTransform))]
	public class ButtonEvent : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

		//--------------- EVENTS ---------------
		//Note these events are not static.
		//A class may subscribe to a button's reference event.
		public delegate void ButtonEventDelegate();
		public event ButtonEventDelegate OnButtonUp;
		public event ButtonEventDelegate OnButtonDown;

		//--------------- OPTIONS ---------------

		[System.Serializable]
		public class SpriteOptions
		{
			public bool useSpriteSwap;

			public Sprite restSprite;
			public Sprite clickSprite;
			public Sprite hoverSprite;
		}

		[System.Serializable]
		public class ColorOptions
		{
			public bool useColorSwap;

			public Color restColor;
			public Color clickColor;
			public Color hoverColor;

			public float transitionTime;
		}

		[System.Serializable]
		public class OrientationOptions
		{
			public bool useOrientationSwap;

			public Orientation restOrientation;
			public Orientation clickOrientation;
			public Orientation hoverOrientation;

			public float transitionTime;
		}

		[System.Serializable]
		public struct Orientation
		{
			public Vector3 euler;
			public Vector3 scale;
		}

		[System.Serializable]
		public class EffectOptions
		{
			public EffectOne effectOne;
			public EffectTwo effectTwo;
			public EffectThree effectThree;
		}

		[System.Serializable]
		public class EffectOne : Effect
		{
			public bool persist;
		}

		[System.Serializable]
		public class EffectTwo : Effect
		{
			public float repeatSpeed;
		}

		[System.Serializable]
		public class EffectThree : Effect
		{
			public Image fillImage2;
			public float alternateSpeed;
		}

		[System.Serializable]
		public class Effect 
		{
			public bool useEffect;
			public float fillSpeed;
			public float dissolveSpeed;
			public Image fillImage;
		}

		//--------------- PUBLIC MEMBERS ---------------
		public string buttonName;
		public SpriteOptions 	  spriteOptions;
		public ColorOptions 	  colorOptions;
		public OrientationOptions orientationOptions;
		public EffectOptions 	  effectOptions;

		[HideInInspector]
		public bool interactable = true;

		//--------------- PROTECTED MEMBERS ---------------
		protected Image _image;

		//--------------- PRIVATE MEMBERS ---------------
		RectTransform   _rect;

		protected bool _onItem; //set during enter and exit events
		protected bool _down;
		protected bool _drag;
		protected bool _effectIsRunning;

		enum ButtonEffect
		{
			NONE,
			ONE,
			TWO,
			THREE
		}
		ButtonEffect _activeEffect;

		///Find the image and rect components of this object
		///An Image component is required since this script handles color and sprite switching
		void Start()
		{
			InitButton();
		}

		protected void InitButton()
		{
			_image = GetComponent<Image>();
			_rect = GetComponent<RectTransform>();
			ApplyItemModifications(spriteOptions.restSprite, colorOptions.restColor, orientationOptions.restOrientation);

			if (effectOptions.effectOne.useEffect)
				_activeEffect = ButtonEffect.ONE;
			else if (effectOptions.effectTwo.useEffect)
				_activeEffect = ButtonEffect.TWO;
			else if (effectOptions.effectThree.useEffect)
				_activeEffect = ButtonEffect.THREE;
		}

		#region --------------- UTILITY MEMBERS ---------------

		///Set the sprite of the image component if sprite switching is enabled
		void SetSprite(Sprite _sprite)
		{
			if (spriteOptions.useSpriteSwap)
			{
				_image.sprite = _sprite;
			}
		}

		///Set the color of the image component if color switching is enabled
		void SetColor(Color _color)
		{
			if (colorOptions.useColorSwap)
			{
				StopCoroutine("MoveColor");
				StartCoroutine("MoveColor", _color);
			}
		}

		//Set the rotation and scale of the component if orienting is enabled
		void SetOrientation(Orientation _orientation)
		{
			if (orientationOptions.useOrientationSwap)
			{
				Debugger.Log("Moving orientation", DebugFlag.TASK);
				StopCoroutine("MoveRotation");
				StartCoroutine("MoveRotation", _orientation);
				StopCoroutine("MoveScale");
				StartCoroutine("MoveScale", _orientation);
			}
		}

		//Sets sprite, color and orientation when an event occurs
		void ApplyItemModifications(Sprite _sprite, Color _color, Orientation _orientation)
		{
			SetSprite(_sprite);
			SetColor(_color);
			SetOrientation(_orientation);
		}

		//Transitions the image's current color to _color in _time amount of time
		IEnumerator MoveColor(Color _color)
		{
			while (!Utility.ColorsMatch(_image.color, _color))
			{
				_image.color = Color.Lerp(_image.color, _color, colorOptions.transitionTime * Time.deltaTime);
				yield return null;
			}
			_image.color = _color;
		}

		//Transitions the rect's euler angles
		IEnumerator MoveRotation(Orientation _orientation)
		{
			Vector3 _eulers = _rect.eulerAngles;
			while (!Utility.VectorsMatch(_rect.eulerAngles, _orientation.euler))
			{
				_eulers.x = Mathf.LerpAngle(_eulers.x, _orientation.euler.x, orientationOptions.transitionTime * Time.deltaTime);
				_eulers.y = Mathf.LerpAngle(_eulers.y, _orientation.euler.y, orientationOptions.transitionTime * Time.deltaTime);
				_eulers.z = Mathf.LerpAngle(_eulers.z, _orientation.euler.z, orientationOptions.transitionTime * Time.deltaTime);
				_rect.eulerAngles = _eulers;
				yield return null;
			}
			_rect.eulerAngles = _orientation.euler;
		}

		//Transitions the rect's scale
		IEnumerator MoveScale(Orientation _orientation)
		{
			Vector3 _scale = _orientation.scale;
			while (!Utility.VectorsMatch(_rect.localScale, _scale))
			{
				_rect.localScale = Vector3.Lerp(_rect.localScale, _scale, orientationOptions.transitionTime * Time.deltaTime);
				yield return null;
			}
			_rect.localScale = _scale;
		}

		IEnumerator RunEffectOne()
		{
			EffectOne effect = effectOptions.effectOne;
			bool run = true;
			bool fill = true;
			Color color = effect.fillImage.color;

			color.a = 1;
			effect.fillImage.color = color;
			effect.fillImage.fillAmount = 0;

			_effectIsRunning = true;

			while (run)
			{
				if (fill) //image is filling up
				{
					effect.fillImage.fillAmount += effect.fillSpeed * Time.deltaTime;

					if (effect.fillImage.fillAmount >= 0.99f)
					{
						fill = effect.persist;
						if (!_onItem && !_drag)
							fill = false; //end the effect if cursor is not on the item at this point
					}
				}
				else //image is fading out
				{
					color.a -= effect.dissolveSpeed * Time.deltaTime;
					effect.fillImage.color = color;

					if (color.a <= 0.01f)
					{
						color.a = 0;
						effect.fillImage.color = color;
						run = false;
					}
				}

				yield return new WaitForSeconds(Time.deltaTime);
			}

			_effectIsRunning = false;
		}

		IEnumerator RunEffectTwo()
		{
			EffectTwo effect = effectOptions.effectTwo;
			bool run = true;
			bool fill = true;
			Color color = effect.fillImage.color;

			color.a = 1;
			effect.fillImage.color = color;
			effect.fillImage.fillAmount = 0;

			_effectIsRunning = true;

			while (run)
			{
				if (fill) //image is filling up
				{
					effect.fillImage.fillAmount += effect.fillSpeed * Time.deltaTime;

					if (effect.fillImage.fillAmount >= 0.99f)
					{
						fill = false;
					}
				}
				else //image is fading out
				{
					color.a -= effect.dissolveSpeed * Time.deltaTime;
					effect.fillImage.color = color;

					if (color.a <= 0.01f)
					{
						color.a = 0;
						effect.fillImage.color = color;

						yield return new WaitForSeconds(effect.repeatSpeed);

						effect.fillImage.fillAmount = 0;
						color.a = 1;
						effect.fillImage.color = color;
						fill = true;

						if (!_onItem && !_drag)
							run = false; //end the effect if cursor is not on the item at this point
					}
				}

				yield return new WaitForSeconds(Time.deltaTime);
			}

			_effectIsRunning = false;
		}

		IEnumerator RunEffectThree()
		{
			EffectThree effect = effectOptions.effectThree;

			_effectIsRunning = true;

			while (true)
			{
				yield return null;
			}

			_effectIsRunning = false;
		}

		IEnumerator EndEffect()
		{
			//wait for effects to stop if they are running
			//effect will force quit when hover is not true on the button
			//this prevents "buggy" looking behavior if we ask the button to stop an effect midway into the effect
			while (_effectIsRunning)
			{
				yield return null; 
			}

			Effect effect = _activeEffect == ButtonEffect.ONE ? (Effect)effectOptions.effectOne :
							_activeEffect == ButtonEffect.TWO ? (Effect)effectOptions.effectTwo :
							_activeEffect == ButtonEffect.THREE ? (Effect)effectOptions.effectThree : null;

			bool run = true;
			Color color = effect.fillImage.color;

			while (run && !_onItem)
			{
				color.a -= effect.dissolveSpeed * Time.deltaTime;
				effect.fillImage.color = color;

				if (color.a <= 0.01f)
				{
					color.a = 0;
					effect.fillImage.color = color;
					run = false;
				}

				yield return new WaitForSeconds(Time.deltaTime);
			}
		}

		protected void HandleButtonEffect()
		{
			if (_drag) //dont trigger an effect when the user is interacting with the button
				return;

			if (effectOptions.effectOne.useEffect)
				_activeEffect = ButtonEffect.ONE;
			else if (effectOptions.effectTwo.useEffect)
				_activeEffect = ButtonEffect.TWO;
			else if (effectOptions.effectThree.useEffect)
				_activeEffect = ButtonEffect.THREE;

			switch (_activeEffect)
			{
				case ButtonEffect.ONE:
					StopCoroutine("RunEffectOne");
					StartCoroutine("RunEffectOne");
					break;
				case ButtonEffect.TWO:
					StopCoroutine("RunEffectTwo");
					StartCoroutine("RunEffectTwo");
					break;
				case ButtonEffect.THREE:
					StopCoroutine("RunEffectThree");
					StartCoroutine("RunEffectThree");
					break;
			}
		}

		protected void StopButtonEffect()
		{
			if (_drag) //dont trigger an effect when the user is interacting with the button
				return;

			if (_activeEffect != ButtonEffect.NONE)
			{
				StopCoroutine("EndEffect");
				StartCoroutine("EndEffect");
			}
		}

		protected void ApplyRestProperties()
		{
			ApplyItemModifications(spriteOptions.restSprite, colorOptions.restColor, orientationOptions.restOrientation);
		}

		protected void ApplyHoverProperties()
		{
			ApplyItemModifications(spriteOptions.hoverSprite, colorOptions.hoverColor, orientationOptions.hoverOrientation);
		}

		protected void ApplyClickProperties()
		{
			ApplyItemModifications(spriteOptions.clickSprite, colorOptions.clickColor, orientationOptions.clickOrientation);
		}

		#endregion

		#region --------------- VIRTUAL MEMBERS ---------------

		/// Members below are intended to be overriden by more specific button functionality
		/// All buttons, however, will apply item modifications based on the parameters set from the inspector

		public virtual void OnItemDrag()
		{
			_drag = true;
			ApplyItemModifications(spriteOptions.clickSprite, colorOptions.clickColor, orientationOptions.clickOrientation);
			Debugger.Log("click", DebugFlag.EVENT);
		}

		public virtual void OnItemDown()
		{
			_down = true;
			ApplyItemModifications(spriteOptions.clickSprite, colorOptions.clickColor, orientationOptions.clickOrientation);
			Debugger.Log("down", DebugFlag.EVENT);
		}

		/// Represents a click event, when the user releases input on a button
		/// If the user is not on the item, no click logic will execute
		public virtual void OnItemUp()
		{
			_drag = false;
			_down = false;
			if (!_onItem)
			{
				ApplyItemModifications(spriteOptions.restSprite, colorOptions.restColor, orientationOptions.restOrientation);
				return;
			}
			else {
				ApplyItemModifications(spriteOptions.hoverSprite, colorOptions.hoverColor, orientationOptions.hoverOrientation);
			}
			
			Debugger.Log("up", DebugFlag.EVENT);
		}

		public virtual void OnItemEnter()
		{
			_onItem = true;
			ApplyHoverProperties();
			Debugger.Log("enter", DebugFlag.EVENT);

			//#if UNITY_EDITOR && !(UNITY_ANDROID || UNITY_IOS)
			HandleButtonEffect();
			//#endif
		}

		public virtual void OnItemExit()
		{
			_onItem = false;
			ApplyRestProperties();
			
			//#if UNITY_EDITOR && !(UNITY_ANDROID || UNITY_IOS)
			StopButtonEffect();
			//#endif
			
			Debugger.Log("exit", DebugFlag.EVENT);
		}

		#endregion


		#region --------------- INTERFACE MEMBERS ---------------

		///Members below call the virtual functions above
		///The base functionality for occurring events is derived from the functions above
		///Any added functionality would be placed in an inheriting class which overrides the virtual functions above

		public void OnDrag(PointerEventData ped)
		{
			if (!interactable) //interaction is not enabled, so we do not continue
				return;

			OnItemDrag();
		}

		public void OnPointerDown(PointerEventData ped)
		{
			if (!interactable) //interaction is not enabled, so we do not continue
				return;

			try {
				//event for foreign classes to conduct some action to this button's object reference
				OnButtonDown();
			} catch (System.NullReferenceException e) {}
			OnItemDown();
		}

		public void OnPointerUp(PointerEventData ped)
		{
			if (!interactable) //interaction is not enabled, so we do not continue
				return;

			try {
				//event for foreign classes to conduct some action to this button's object reference
				OnButtonUp();
			} catch (System.NullReferenceException e) {}
			OnItemUp();
		}

		public void OnPointerEnter(PointerEventData ped)
		{
			if (!interactable) //interaction is not enabled, so we do not continue
				return;

			OnItemEnter();
		}

		public void OnPointerExit(PointerEventData ped)
		{
			OnItemExit();
		}

		#endregion

	}
}
