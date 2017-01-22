using UnityEngine;
using System.Collections;

public class ExplosionParticles : SimpleParticleSystem {

	[System.Serializable]
	public class Settings
	{
		public float radius;
	}

	public Settings settings = new Settings();

	void Start()
	{
		Init();
		//SpawnParticles();
	}

	public void SpawnParticles()
	{
		Lightning.Instance.Flash();
		int i = 0;
		while (i < maxParticles)
		{
			float randomAngle = Random.Range(0, 359);
			Vector3 targetPos = settings.radius * transform.right;
			targetPos = transform.position + Quaternion.Euler(transform.forward * randomAngle) * targetPos;
			Vector3 newRot = new Vector3(0, 0, randomAngle);
			float randomVel = Random.Range(0.1f, velocity);
			Vector3 dir = (targetPos - transform.position) * randomVel;
			SpawnParticle(transform.position, newRot, dir);
			i++;
		}
	}

	void Update()
	{
		UpdateParticles();
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			//SpawnParticles();
			Lightning.Instance.Flash();
		}
	}
}
