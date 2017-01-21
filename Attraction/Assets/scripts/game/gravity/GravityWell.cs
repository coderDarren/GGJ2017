using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {

	public delegate void ProximityDelegate(GravityWell influencer);
	public static event ProximityDelegate PlayerEntered;
	public static event ProximityDelegate PlayerLeft;

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
	float playerScale;
	Vector3 relativeDirection;

	int influence;
	int wellsInfluencingPlayer;
	bool containsPlayer;
	bool destroyPlayer;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		influence = gravityType == GravityType.NORMAL ? 1 : -1;
		playerScale = 0.2f;
	}

	void OnEnable()
	{
		GravityWell.PlayerEntered += OnPlayerEntered;
		GravityWell.PlayerLeft += OnPlayerLeft;
	}

	void OnDisable()
	{
		GravityWell.PlayerEntered -= OnPlayerEntered;
		GravityWell.PlayerLeft -= OnPlayerLeft;
	}

	void OnPlayerEntered(GravityWell well)
	{
		if (this == well)
			containsPlayer = true;
		wellsInfluencingPlayer++;
	}

	void OnPlayerLeft(GravityWell well)
	{
		if (this == well)
			containsPlayer = false;
		wellsInfluencingPlayer--;
	}

	void Update()
	{
		distance = Vector3.Distance(player.position, transform.position);

		if (distance < radius / 6) {
			player.GetComponent<ShipController>().power = 0;
			player.GetComponent<ShipController>().coast = 0;
			destroyPlayer = true;
		}

		if (distance < radius){
			if (!containsPlayer)
				PlayerEntered(this);
			magnitude = (radius - distance) * 2;

		}
		else{
			if (containsPlayer)
				PlayerLeft(this);
			magnitude = 0;
		}

		relativeDirection = transform.position - player.position;
		angleToPlayer = Mathf.Atan2(relativeDirection.y, relativeDirection.x) * Mathf.Rad2Deg;
     	Quaternion target = Quaternion.Euler(0f, 0f, angleToPlayer - 90 * influence);
     	player.rotation = Quaternion.Lerp(player.rotation, target, magnitude * force * Time.deltaTime);

     	if (containsPlayer || wellsInfluencingPlayer == 0) {
     		HandlePlayerScale();
     	}
	}

	void HandlePlayerScale()
	{
		if (distance < radius){
			playerScale = (distance * 0.15f) / radius;
		}
		else{
			playerScale = 0.15f;
		}
		if (destroyPlayer)
			playerScale = 0;
		player.localScale = Vector3.Lerp(player.localScale, Vector3.one * playerScale, 1.5f * Time.deltaTime);
	}
}
