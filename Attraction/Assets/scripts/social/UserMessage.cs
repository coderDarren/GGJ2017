using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class UserMessage : MonoBehaviour {

	Text t;
	SessionManager session;

	void OnEnable() {
		session = SessionManager.Instance;
		t = GetComponent<Text>();
		OnRefreshUsability();
		SessionManager.RefreshSocialUsability += OnRefreshUsability;
	}

	void OnDisable() {
		SessionManager.RefreshSocialUsability -= OnRefreshUsability;
	}

	void OnRefreshUsability() {
		if (session.validUser) {
			t.text = "Hello,\n" + session.userName;
		} else {
			t.text = "Not logged in.\nProgress will not be linked to your account.";
		}
	}
}
