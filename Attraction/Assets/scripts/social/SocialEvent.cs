using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;

public class SocialEvent : ButtonEvent {

	public enum SocialEventType {
		LOGIN,
		ACHIEVEMENTS,
		LEADERBOARD
	}
	public SocialEventType eventType;
	public Color enabledColor, disabledColor;
	public Image icon;

	SessionManager session;

	void OnEnable() {
		session = SessionManager.Instance;
		InitButton();
		OnRefreshUsability();
		SessionManager.RefreshSocialUsability += OnRefreshUsability;
	}

	void OnDisable() {
		SessionManager.RefreshSocialUsability -= OnRefreshUsability;
	}

	void OnRefreshUsability() {
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
		}
	}
}
