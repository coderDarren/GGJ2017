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
				s = new Ship(ship, "Trainee Shuttle", 1, 1f, 0.01f, 0.01f, 20, sprite, 100);
				break;
			case ShipType.SHIP_02: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/5points");
				s = new Ship(ship, "The Fighter", 1, 1f, 0.01f, 0.01f, 20, sprite, 1005);
				break;
			case ShipType.SHIP_03: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/arrowWing");
				s = new Ship(ship, "The Arrow", 1, 1f, 0.01f, 0.01f, 20, sprite, 2550);
				break;
			case ShipType.SHIP_04: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/blades");
				s = new Ship(ship, "The Collector", 1, 1f, 0.01f, 0.01f, 20, sprite, 2720);
				break;
			case ShipType.SHIP_05: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/explorer");
				s = new Ship(ship, "The Explorer", 1, 1f, 0.01f, 0.01f, 20, sprite, 6500);
				break;
			case ShipType.SHIP_06: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/falcon");
				s = new Ship(ship, "The Falcon", 1, 1f, 0.01f, 0.01f, 20, sprite, 6950);
				break;
			case ShipType.SHIP_07: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/theBull");
				s = new Ship(ship, "The Bull", 1, 1f, 0.01f, 0.01f, 20, sprite, 10350);
				break;
			case ShipType.SHIP_08: 
				//shipType, name, coast speed, thrust power, thrust accel, accel, armor, sprite, cost
				sprite = Resources.Load<Sprite>("sprites/pinser");
				s = new Ship(ship, "The Pinser", 1, 1f, 0.01f, 0.01f, 20, sprite, 12990);
				break;
		}
		
		return s;
	}
}
