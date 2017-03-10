using UnityEngine;
using System.Collections;
using Types;

[RequireComponent(typeof(SpriteRenderer))]
public class ResourcePickup : SimpleParticleSystem {

	[System.Serializable]
	public class Settings {
		public float radius;
	}

	public Settings settings = new Settings();
	public int minResources;
	public int maxResources;
	[Range(0.01f, 1)]
	public float dropChance;

	bool shouldEmit = true;
	int resources;

	void Awake() {
		float chance = Random.Range(0.01f, 1);
		if (chance > dropChance) {
			Destroy(gameObject);
		}
	}

	void Start()
	{
		resources = Random.Range(minResources, maxResources);
		Init();
		StartCoroutine("SpawnParticles");
	}

	IEnumerator SpawnParticles()
	{
		while (shouldEmit)
		{
			float randomAngle = Random.Range(0, 359);
			Vector3 newPos = settings.radius * transform.right;
			newPos = transform.position + Quaternion.Euler(transform.forward * randomAngle) * newPos;
			Vector3 newRot = new Vector3(0, 0, randomAngle);
			Vector3 dir = newPos - transform.position;
			SpawnParticle(newPos, newRot, dir);
			yield return new WaitForSeconds(spawnRate);
		}
	}

	void Update()
	{
		UpdateParticles();
	}

	IEnumerator WaitToDie() {
		while (particles.Count > 0) {
			yield return null;
		}
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!shouldEmit) return;
		if (col.gameObject.tag == "Player") {

			/*if (!TutorialManager.Instance.TutorialIsComplete(TutorialType.COLLECT_RESOURCES)) {
				col.gameObject.GetComponent<ShipController>().Pause();
				TutorialManager.Instance.TryLaunchTutorial(TutorialType.COLLECT_RESOURCES);
			}*/

			shouldEmit = false;
			GetComponent<SpriteRenderer>().enabled = false;
			ProgressManager.Instance.AddResources((uint)resources);
			StartCoroutine("WaitToDie");
		}
	}
}
