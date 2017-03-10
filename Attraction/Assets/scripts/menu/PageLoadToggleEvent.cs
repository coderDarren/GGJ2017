using UnityEngine;
using System.Collections;
using DebugServices;

namespace Menu {

	public class PageLoadToggleEvent : ButtonEvent {

		public delegate void ToggleEvent(ButtonEvent be, int id);
		public static event ToggleEvent ToggleThis;

		public int toggleGroupId = 0;
		public PageType pageToLoad;
		public bool toggleThisFirst;

		void Start()
		{
			if (toggleThisFirst)
				OnItemUp();
		}

		void OnEnable()
		{
			PageLoadToggleEvent.ToggleThis += OnToggleThis;
		}

		void OnDisable()
		{
			PageLoadToggleEvent.ToggleThis -= OnToggleThis;
		}

		public virtual void OnToggleThis(ButtonEvent be, int id)
		{
			if (id != toggleGroupId)
				return; //event not intended for this toggle group

			if (this == be) //this button was activated
			{
				_onItem = true;
				ApplyHoverProperties();
				HandleButtonEffect();
			}
			else { //another button was activated
				_onItem = false;
				ApplyRestProperties();
				StopButtonEffect();
				PageManager.Instance.TurnOffPage(pageToLoad, PageType.NONE);
			}
		}

		protected void Toggle(ButtonEvent be, int id) {
			ToggleThis(this, toggleGroupId); //notify other buttons with this group identifier
		}

		public override void OnItemUp()
		{
			Toggle(this, toggleGroupId);
			PageManager.Instance.LoadPage(pageToLoad);
		}

		public override void OnItemEnter()
		{
		}

		public override void OnItemExit()
		{
		}

		public override void OnItemDrag()
		{
		}

		public override void OnItemDown()
		{
		}

	}
}