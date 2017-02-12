using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;

public class SocialEvent : ButtonEvent {

	public enum SocialEventType {
		LOGIN,
		ACHIEVEMENTS,
		LEADERBOARD,
		LEADERBOARD_STARS,
		LEADERBOARD_RESOURCES
	}
	public SocialEventType eventType;
	public Color enabledColor, disabledColor;
	public Image icon;

	SessionManager session;

	void Start() {
		session = SessionManager.Instance;
		InitButton();
		Init();
	}

	void Init() {
		if (eventType != SocialEventType.LOGIN) {
			if (session.validUser) {
				_image.color = enabledColor;
				icon.color = enabledColor;
				interactable = true;
			} else {
				_image.color = disabledColor;
				icon.color = disabledColor;
				interactable = false;
			}
		}
	}

	public override void OnItemUp() {
		base.OnItemUp();
		switch (eventType) {
			case SocialEventType.LOGIN: session.HandleLogin(); break;
			case SocialEventType.ACHIEVEMENTS: session.ShowAchievements(); break;
			case SocialEventType.LEADERBOARD: session.ShowLeaderboard(); break;
			case SocialEventType.LEADERBOARD_RESOURCES: session.ShowLeaderboard(Remnant.GPGSIds.leaderboard_resources_earned); break;
			case SocialEventType.LEADERBOARD_STARS: session.ShowLeaderboard(Remnant.GPGSIds.leaderboard_stars_earned); break;
		}
	}
}
