using UnityEngine;
using System.Collections;
using Types;

public class ShipFinder  {

	public static Ship GetShip(ShipType ship) {
		Ship s = null;
		Sprite sprite = null;

		switch (ship) {
			case ShipType.SHIP_01: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/icon_ship_04");
				s = new Ship(ship, "Trainee Shuttle", 1, 1, 0.001f, 0.01f, 20, sprite, 100);
				break;
			case ShipType.SHIP_02: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/5points");
				s = new Ship(ship, "The Fighter", 1, 1.25f, 0.0015f, 0.015f, 10, sprite, 1005);
				break;
			case ShipType.SHIP_03: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/arrowWing");
				s = new Ship(ship, "The Arrow", 2, 1.5f, 0.001f, 0.02f, 4, sprite, 2550);
				break;
			case ShipType.SHIP_04: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/blades");
				s = new Ship(ship, "The Collector", 0.75f, 1.15f, 0.00075f, 0.005f, 12, sprite, 2720);
				break;
			case ShipType.SHIP_05: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/explorer");
				s = new Ship(ship, "The Explorer", 0.5f, 3, 0.00075f, 0.02f, 24, sprite, 6500);
				break;
			case ShipType.SHIP_06: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/falcon");
				s = new Ship(ship, "The Falcon", 1.5f, 1.5f, 0.0015f, 0.01f, 12, sprite, 6950);
				break;
			case ShipType.SHIP_07: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/theBull");
				s = new Ship(ship, "The Bull", 3, 2.5f, 0.003f, 0.03f, 6, sprite, 10350);
				break;
			case ShipType.SHIP_08: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/pinser");
				s = new Ship(ship, "The Pinser", 4, 1.5f, 0.002f, 0.02f, 8, sprite, 12990);
				break;
		}
		
		return s;
	}
}
