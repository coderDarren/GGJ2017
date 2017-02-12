using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class UserMessage : MonoBehaviour {

	Text t;
	SessionManager session;

	void Start() {
		session = SessionManager.Instance;
		t = GetComponent<Text>();
		Init();
	}

	void Init() {
		if (session.validUser) {
			t.text = "Hello,\n" + session.userName;
		} else {
			t.text = "Logged in as Guest.\nProgress will not be linked to any account.";
		}
	}
}
