using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class PlayerName : MonoBehaviour {

	void Start() {
		GetComponent<Text>().text = SessionManager.Instance.userName;
	}

}
