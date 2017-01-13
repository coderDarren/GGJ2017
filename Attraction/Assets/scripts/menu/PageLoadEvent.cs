using UnityEngine;
using System.Collections;
using Debug;

namespace Menu {

	public class PageLoadEvent : ButtonEvent {

		public PageType pageToLoad;
		public PageType pageToRemove;

		public override void OnItemUp()
		{
			if (pageToRemove != PageType.NONE)
			{
				Debugger.Log("Removing Page: "+pageToRemove, DebugFlag.TASK);
				PageManager.Instance.TurnOffPage(pageToRemove, pageToLoad);
			}
			else
			{
				PageManager.Instance.LoadPage(pageToLoad);
			}

			base.OnItemUp();
		}
	}
}
