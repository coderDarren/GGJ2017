using UnityEngine;
using System.Collections;

public class shipController : MonoBehaviour {

	public float movementSpeed;
	public GameObject rocket;
	public Transform shotPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A)) {
			transform.position -= transform.right * Time.deltaTime * movementSpeed;
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.position += transform.right * Time.deltaTime * movementSpeed;
		}
		if(Input.GetMouseButtonDown(0)) {
			CreateRocket();
		}
	}

	void CreateRocket() {
		GameObject rocketClone = (GameObject)Instantiate(rocket, shotPos.position, shotPos.rotation);
	}
}
