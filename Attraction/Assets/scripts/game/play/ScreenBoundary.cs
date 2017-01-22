using UnityEngine;
using System.Collections;

public class ScreenBoundary : MonoBehaviour {

	public ExplosionParticles explosion;
	public GameObject playerRef;

	ShipController ship;

	void Start()
	{
		playerRef = GameObject.FindGameObjectWithTag("Player");
		ship = playerRef.GetComponent<ShipController>();
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Player")) {
			if (ship.dying || ship.state == ShipController.State.RESET)
				return;
			explosion.gameObject.transform.position = col.gameObject.transform.position;
			explosion.SpawnParticles();
			playerRef.GetComponent<ShipController>().ReduceLives();
		}
	}
}
