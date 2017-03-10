using UnityEngine;
using System.Collections;
using Types;
using Menu;

public class MenuController : PageController {

	TutorialManager tutorial;

	void Start () {
		tutorial = TutorialManager.Instance;
		Init();
	}

	void Update() {
		UpdatePage();
	}

	public override void OnPageDidEnter() {
		/*if (!tutorial.TutorialIsComplete(TutorialType.BUY_SHIP))
			tutorial.TryLaunchTutorial(TutorialType.BUY_SHIP);
		else
			tutorial.TryLaunchTutorial(TutorialType.SHIP_ARMOR);*/
	}

}
