using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Util;
using PoolingServices;

public class SimpleParticleSystemUI : MonoBehaviour {

	public class Particle
	{
		public Image image;
		public Vector3 direction;
		public float velocity;
		public float acceleration;
		public float lifeTime;
		public bool alive;
		public Color targetColor;
		public Color currentColor;

		public Vector3 sizeVel;	//for smooth damp purposes
		public Vector4 colorVel;	//for smooth damp purposes

		float lifeSpan;
		Color[] colors;
		int currentColorIndex;
		RectTransform rect;

		public int poolId { get; private set; }

		public Vector3 Position
		{
			get { return rect.transform.position; }
			set { rect.transform.position = value; }
		}

		public Vector3 EulerAngles
		{
			get { return rect.transform.eulerAngles; }
			set { rect.transform.eulerAngles = value; }
		}

		public Vector3 Size
		{
			get { return rect.transform.localScale; }
			set { rect.transform.localScale = value; }
		}

		public Color SpriteColor
		{
			get { return image.color; }
			set { image.color = value; }
		}

		public Particle(Image img, int poolId, Vector3 dir, float vel, float accel, float life, Color[] col)
		{
			this.poolId = poolId;
			image = img;
			rect = image.GetComponent<RectTransform>();
			direction = dir;
			velocity = vel;
			acceleration = accel;
			lifeSpan = life;
			colors = col;
			targetColor = colors[0];
			currentColor = Color.white;
			alive = true;
			lifeTime = 0;
			currentColorIndex = 0;
			sizeVel = Vector3.zero;
			colorVel = Vector4.zero;
		}

		public void TickColor()
		{
			if (Utility.ColorsMatch(targetColor, SpriteColor))
			{
				currentColorIndex++;
				if (currentColorIndex > colors.Length - 1) {
					SpriteColor = targetColor;
				} else {
					targetColor = colors[currentColorIndex];
				}
			}
		}

		public void TickAccel()
		{
			velocity += acceleration;
			if (velocity <= 0)
				velocity = 0;	
		}

		public void TickLife()
		{
			lifeTime += Time.deltaTime;
			if (lifeTime >= lifeSpan) {
				alive = false;
			}
		}
	}

	public GameObject particle;
	public Transform parent;
	public int maxParticles;
	public float lifeTime;
	public float spawnRate;
	public float startSizeMin;
	public float startSizeMax;
	public float endSize;
	public float velocity;
	public float acceleration;
	public Color startColor;
	public Color[] colors;

	protected List<Particle> particles;
	Pool pool;

	protected void Init()
	{
		particles = new List<Particle>();
		pool = new Pool();
		pool.ConfigurePool(parent, particle, maxParticles);
	}

	protected void SpawnParticle(Vector3 pos, Vector3 rot, Vector3 dir)
	{
		//GameObject particleObj = (GameObject)Instantiate(particle);
		//particleObj.transform.parent = parent;
		int poolObjId = pool.GetObject();
		if (poolObjId == -1) //no available objects left in the pool
			return;
		Transform particleObj = pool.ObjectTransform(poolObjId);
		Particle p = new Particle(particleObj.GetComponent<Image>(), poolObjId, dir, velocity, acceleration, lifeTime, colors);
		p.Position = pos;
		p.EulerAngles = rot;
		p.Size = Vector3.one * Random.Range(startSizeMin, startSizeMax);
		p.SpriteColor = startColor;
		p.currentColor = startColor;
		particles.Add(p);
	}

	protected void UpdateParticles()
	{
		for (int i = particles.Count - 1; i >= 0; i--)
		{
			//size
			particles[i].Size = Vector3.SmoothDamp(particles[i].Size, Vector3.one * endSize, ref particles[i].sizeVel, lifeTime - particles[i].lifeTime);

			//color
			particles[i].currentColor.r = Mathf.SmoothDamp(particles[i].currentColor.r, particles[i].targetColor.r, ref particles[i].colorVel.x, 0.25f);
			particles[i].currentColor.g = Mathf.SmoothDamp(particles[i].currentColor.g, particles[i].targetColor.g, ref particles[i].colorVel.y, 0.25f);
			particles[i].currentColor.b = Mathf.SmoothDamp(particles[i].currentColor.b, particles[i].targetColor.b, ref particles[i].colorVel.z, 0.25f);
			particles[i].currentColor.a = Mathf.SmoothDamp(particles[i].currentColor.a, particles[i].targetColor.a, ref particles[i].colorVel.w, 0.25f);
			particles[i].SpriteColor = particles[i].currentColor;
			particles[i].TickColor();

			//rotation

			//position
			particles[i].Position += particles[i].direction * particles[i].velocity * Time.deltaTime;
			particles[i].TickAccel();

			particles[i] = particles[i];

			particles[i].TickLife();
			if (!particles[i].alive) {
				pool.DiscardObject(particles[i].poolId);
				particles.RemoveAt(i);
			}
		}
	}

}
