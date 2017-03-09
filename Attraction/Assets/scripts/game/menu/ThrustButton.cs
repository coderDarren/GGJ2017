using UnityEngine;
using System.Collections;
using Menu;

[RequireComponent(typeof(GravityWellParticlesUI))]
public class ThrustButton : ButtonEvent {

	GravityWellParticlesUI particles;
	ShipController ship;

	void Start() {
		InitButton();
		particles = GetComponent<GravityWellParticlesUI>();
		ship = GameObject.FindObjectOfType<ShipController>();
	}

	public override void OnItemDown() {
		base.OnItemDown();
		particles.startSizeMin = 0.1f;
		particles.startSizeMax = 0.2f;
		particles.velocity = -5;
		ship.thrustersEngage = true;
	}

	public override void OnItemUp() {
		base.OnItemUp();
		particles.startSizeMin = 0.05f;
		particles.startSizeMax = 0.08f;
		particles.velocity = -0.2f;
		ship.thrustersEngage = false;
	}
}
