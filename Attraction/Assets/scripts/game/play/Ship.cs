using UnityEngine;
using System.Collections;
using Types;

public class Ship {

	public ShipType shipType { get; private set; }
	public string name { get; private set; }
	public float coast { get; private set; }
	public float thrustPower { get; private set; }
	public float thrustAccel { get; private set; }
	public float acceleration { get; private set; }
	public int armor { get; private set; }
	public int cost { get; private set; }
	public Sprite sprite { get; private set; }
	public int lives { get { return ProgressManager.Instance.ShipLives(shipType); } }
	public bool purchased { get { return ProgressManager.Instance.ShipWasPurchased(shipType); } }

	public Ship(
		   ShipType shipType,
		   string name,
		   float coast,
		   float thrustPower, 
		   float thrustAccel,
		   float acceleration,
		   int armor,
		   Sprite sprite,
		   int cost) 
	{
		this.shipType = shipType;
		this.name = name;
		this.coast = coast;
		this.thrustPower = thrustPower;
		this.thrustAccel = thrustAccel;
		this.acceleration = acceleration;
		this.armor = armor;
		this.sprite = sprite;
		this.cost = cost;
	}
}
