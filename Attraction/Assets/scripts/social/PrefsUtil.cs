using UnityEngine;
using System.Collections;
using Types;

public class PrefsUtil  {

	public static string ShipLivesId(ShipType ship) {
		switch (ship) {
			case ShipType.SHIP_01: return "ship_01_lives";
			case ShipType.SHIP_02: return "ship_02_lives";
			case ShipType.SHIP_03: return "ship_03_lives";
			case ShipType.SHIP_04: return "ship_04_lives";
			case ShipType.SHIP_05: return "ship_05_lives";
			case ShipType.SHIP_06: return "ship_06_lives";
			case ShipType.SHIP_07: return "ship_07_lives";
			case ShipType.SHIP_08: return "ship_08_lives";
			default: return string.Empty;
		}
	}

	public static string ShipArmorTimestamp(ShipType ship) {
		switch (ship) {
			case ShipType.SHIP_01: return TimestampId(TimestampType.SHIP_01_ARMOR);
			case ShipType.SHIP_02: return TimestampId(TimestampType.SHIP_02_ARMOR);
			case ShipType.SHIP_03: return TimestampId(TimestampType.SHIP_03_ARMOR);
			case ShipType.SHIP_04: return TimestampId(TimestampType.SHIP_04_ARMOR);
			case ShipType.SHIP_05: return TimestampId(TimestampType.SHIP_05_ARMOR);
			case ShipType.SHIP_06: return TimestampId(TimestampType.SHIP_06_ARMOR);
			case ShipType.SHIP_07: return TimestampId(TimestampType.SHIP_07_ARMOR);
			case ShipType.SHIP_08: return TimestampId(TimestampType.SHIP_08_ARMOR);
			default: return string.Empty;
		}
	}

	public static string TimestampId(TimestampType timeStamp) {
		switch (timeStamp) {
			case TimestampType.SHIP_01_ARMOR: return "timestamp_ship_01_armor";
			case TimestampType.SHIP_02_ARMOR: return "timestamp_ship_02_armor";
			case TimestampType.SHIP_03_ARMOR: return "timestamp_ship_03_armor";
			case TimestampType.SHIP_04_ARMOR: return "timestamp_ship_04_armor";
			case TimestampType.SHIP_05_ARMOR: return "timestamp_ship_05_armor";
			case TimestampType.SHIP_06_ARMOR: return "timestamp_ship_06_armor";
			case TimestampType.SHIP_07_ARMOR: return "timestamp_ship_07_armor";
			case TimestampType.SHIP_08_ARMOR: return "timestamp_ship_08_armor";
			default: return string.Empty;
		}
	}

	public static string MiscId(MiscType misc) {
		switch (misc) {
			case MiscType.PLAYER_SHIP_TYPE: return "player_ship_type";
			default: return string.Empty;
		}
	}

}
