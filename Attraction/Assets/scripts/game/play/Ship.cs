using UnityEngine;
using System.Collections;
using Types;

public class Ship {

	public ShipType shipType { get; private set; }
	public float power { get; private set; }
	public float thrust { get; private set; }
	public float acceleration { get; private set; }
	public int armor { get; private set; }
	public int lives { get { return ProgressManager.Instance.ShipLives(shipType); } }
	public bool purchased { get { return ProgressManager.Instance.ShipWasPurchased(shipType); } }

	public Ship(
		   ShipType shipType,
		   float power, 
		   float thrust,
		   float acceleration,
		   int armor) 
	{
		this.shipType = shipType;
		this.power = power;
		this.thrust = thrust;
		this.acceleration = acceleration;
		this.armor = armor;
	}
}
