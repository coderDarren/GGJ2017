using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleParticleSystem : MonoBehaviour {

	public struct Particle 
	{
		public SpriteRenderer sprite;
		public Transform transform;
		public bool alive;
		public Color targetColor;
		public Particle(SpriteRenderer sr, Transform t)
		{
			sprite = sr;
			transform = t;
			alive = false;
			targetColor = sprite.color;
		}
	}

	public SpriteRenderer particle;
	public Transform parent;
	public float lifeTime;
	public float spawnRate;
	public float startSizeMin;
	public float startSizeMax;
	public float endSize;
	public Color startColor;
	public Color endColor;

	List<Particle> particles;

	protected void SpawnParticle(Vector3 pos, Vector3 scale, Vector3 rot)
	{
		SpriteRenderer sr = (SpriteRenderer)Instantiate(particle);
		Particle p = new Particle(sr, sr.transform);
		p.transform.position = pos;
		p.transform.eulerAngles = rot;
		p.transform.localScale = scale;
		particles.Add(p);
	}

	protected void Update()
	{
		for (int i = 0; i < particles.Count; i++)
		{
			//size
		}
	}

	protected void DestroyDeadParticles()
	{
		for (int i = particles.Count - 1; i >= 0; i--)
		{
			if (particles[i].alive == false)
			{
				Destroy(particles[i].sprite);
				particles.RemoveAt(i);
			}
		}
	}
}
