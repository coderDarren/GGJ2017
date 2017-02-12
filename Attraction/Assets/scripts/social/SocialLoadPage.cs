using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Menu;

public class SocialLoadPage : MonoBehaviour {

	public Text userMessage;

	void Start() {
		userMessage.text = SessionManager.Instance.userName == "Guest" ? 
						   "Logging out" :
						   "Hello, "+SessionManager.Instance.userName+".\nWe are collecting your game data.";
		StartCoroutine("LoadUser");
	}	

	IEnumerator LoadUser() {
		yield return new WaitForSeconds(2);
		DataStorage.LoadUser(SessionManager.Instance.userId, false);
		while (DataStorage.LOADING_USER) {
			yield return new WaitForSeconds(0.1f);
		}
		PageManager.Instance.TurnOffPage(PageType.SOCIAL_LOAD, PageType.SOCIAL);
	}
}
