using UnityEngine;
using System.Collections;
using Types;

public class ShipFinder  {

	public static Ship GetShip(ShipType ship) {
		Ship s = null;

		switch (ship) {
			case ShipType.SHIP_01: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_02: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_03: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_04: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_05: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_06: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_07: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
			case ShipType.SHIP_08: 
				//shipType, power, thrust, accel, armor
				s = new Ship(ship, 1, 0.001f, 0.01f, 20);
				break;
		}
		
		return s;
	}
}
