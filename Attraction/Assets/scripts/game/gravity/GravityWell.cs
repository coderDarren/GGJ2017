using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {

	public enum GravityType {
		NORMAL,
		ANTI_GRAVITY
	}
	public GravityType gravityType;

	public float radius;
	public float force;

	Transform player;
	float distance;
	float magnitude;
	float angleToPlayer;
	Vector3 relativeDirection;

	int influence;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		influence = gravityType == GravityType.NORMAL ? 1 : -1;
	}

	void Update()
	{
		distance = Vector3.Distance(player.position, transform.position);

		if (distance < radius)
			magnitude = (radius - distance) * 2;
		else
			magnitude = 0;

		relativeDirection = transform.position - player.position;
		angleToPlayer = Mathf.Atan2(relativeDirection.y, relativeDirection.x) * Mathf.Rad2Deg;
     	Quaternion target = Quaternion.Euler(0f, 0f, angleToPlayer - 90 * influence);
     	player.rotation = Quaternion.Lerp(player.rotation, target, magnitude * force * Time.deltaTime);
	}
}
