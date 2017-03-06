using UnityEngine;
using System.Collections;

public class asteroidSpawner : MonoBehaviour {

	float timer;
	public GameObject asteroid;

	void Start () {
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0) {
			CreateAsteroid();
			ResetTimer();
		}
	}

	void CreateAsteroid () {
		GameObject asteroidClone = (GameObject)Instantiate(asteroid, transform.position, transform.rotation);
	}

	void ResetTimer() {
		timer = Random.Range(1f, 5f);
	}
}
