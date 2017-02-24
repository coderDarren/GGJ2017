using UnityEngine;
using System.Collections;
using System;
using Types;
using Util;

public class ProgressManager : MonoBehaviour {

	const int MINUTES_TO_SHIP_RECHARGE = 5;
	int leftOverMinutes = 0;

	public delegate void UIStatTextDelegate(int amount);
	public event UIStatTextDelegate UpdateStarsText;
	public event UIStatTextDelegate UpdateResourcesText;

	public static ProgressManager Instance;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public void ResetProgress()
	{
		DataStorage.LoadUser(SessionManager.Instance.userId, true);
	}

	public GalaxyType PreviousGalaxy(GalaxyType galaxy) {
		switch (galaxy) {
			case GalaxyType.DAHKRI_GALAXY: return GalaxyType.HOME_GALAXY;
			case GalaxyType.KYDOR_GALAXY: return GalaxyType.VIDON_GALAXY;
			case GalaxyType.ZAX_GALAXY: return GalaxyType.XILYANTIPHOR_GALAXY;
			case GalaxyType.MALIX_GALAXY: return GalaxyType.RYKTAR_GALAXY;
			case GalaxyType.XILYANTIPHOR_GALAXY: return GalaxyType.DAHKRI_GALAXY;
			case GalaxyType.VIDON_GALAXY: return GalaxyType.ZAX_GALAXY;
			case GalaxyType.RYKTAR_GALAXY: return GalaxyType.KYDOR_GALAXY;
			default: 
				return GalaxyType.NONE;
		}
	}

	public GalaxyType NextGalaxy(GalaxyType galaxy) {
		switch (galaxy) {
			case GalaxyType.DAHKRI_GALAXY: return GalaxyType.XILYANTIPHOR_GALAXY;
			case GalaxyType.KYDOR_GALAXY: return GalaxyType.RYKTAR_GALAXY;
			case GalaxyType.ZAX_GALAXY: return GalaxyType.VIDON_GALAXY;
			case GalaxyType.HOME_GALAXY: return GalaxyType.DAHKRI_GALAXY;
			case GalaxyType.XILYANTIPHOR_GALAXY: return GalaxyType.ZAX_GALAXY;
			case GalaxyType.VIDON_GALAXY: return GalaxyType.KYDOR_GALAXY;
			case GalaxyType.RYKTAR_GALAXY: return GalaxyType.MALIX_GALAXY;
			default:
				return GalaxyType.NONE;
		}
	}

	public bool GalaxyIsAvailable(GalaxyType galaxy)
	{
		if (galaxy == GalaxyType.HOME_GALAXY) return true;

		GalaxyType previousGalaxy = PreviousGalaxy(galaxy);
		int stars = GetLevelStars(previousGalaxy, 5);

		if (stars > 0) {
			return true;
		}

		return false;
	}

	public bool LevelIsAvailable(GalaxyType galaxy, int level) 
	{
		//if galaxy does not exist
		if (galaxy == GalaxyType.NONE) return false;

		//if level is 1, return true
		if (level == 1) return true;

		//otherwise we need to check if the previous level has been completed
		int checkLevel = level - 1;
		int stars = GetLevelStars(galaxy, checkLevel);
		
		if (stars > 0) {
			return true;
		}

		return false;
	}

	public bool LevelHasBeenAttempted(GalaxyType galaxy, int level) 
	{
		string dataId = GPGSUtil.GalaxyLevelAttemptsId(galaxy, level);
		int attempts = DataStorage.GetLocalData(dataId);
		if (attempts > 0) {
			return true;
		}
		return false;
	}

	public int GetLevelStars(GalaxyType galaxy, int level)
	{
		string dataId = GPGSUtil.GalaxyLevelStarsId(galaxy, level);
		return DataStorage.GetLocalData(dataId);
	}

	public void MarkLevelAttempted(GalaxyType galaxy, int level)
	{
		string dataId = GPGSUtil.GalaxyLevelAttemptsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, 1);
	}

	public void MarkLevelStars(GalaxyType galaxy, int level, int stars)
	{
		string dataId = GPGSUtil.GalaxyLevelStarsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, (uint)stars);
	}

	public void MarkLevelWins(GalaxyType galaxy, int level)
	{
		string dataId = GPGSUtil.GalaxyLevelWinsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, 1);
	}

	public void MarkLevelAchievement(GalaxyType galaxy, int level) 
	{
		DataStorage.IncrementLevelAchievement(galaxy, level, 1);
	}

	public void MarkShipPurchased(ShipType ship) 
	{
		string dataId = GPGSUtil.ShipId(ship);
		int info = DataStorage.GetLocalData(dataId);
		if (info < 1) {
			DataStorage.IncrementEvent(dataId, 1);
		}
	}

	public bool ShipWasPurchased(ShipType ship)
	{
		string dataId = GPGSUtil.ShipId(ship);
		if (dataId == string.Empty) return false;
		int info = DataStorage.GetLocalData(dataId);
		if (info == 1) return true;
		return false;
	}

	public int ShipLives(ShipType ship) 
	{
		string dataId = PrefsUtil.ShipLivesId(ship);
		if (dataId == string.Empty) return 0;
		return DataStorage.GetLocalData(dataId);
	}

	public void SetShipLives(ShipType ship, int newLifeCount) 
	{
		int armor = ShipFinder.GetShip(ship).armor;
		if (newLifeCount > armor) newLifeCount = armor;
		if (newLifeCount < 0) newLifeCount = 0;
		string dataId = PrefsUtil.ShipLivesId(ship);
		if (dataId == string.Empty) return;
		DataStorage.SaveLocalData(dataId, newLifeCount);
	}

	public int MinutesLeftToNextRecharge(ShipType ship)
	{
		string timestampId = PrefsUtil.ShipArmorTimestamp(ship);
		int minutesPassed = TimeUtil.MinutesSinceDateTime(timestampId) ;
		return (MINUTES_TO_SHIP_RECHARGE - 1) - minutesPassed;
	}

	public int SecondsLeftToNextRecharge(ShipType ship)
	{
		string timestampId = PrefsUtil.ShipArmorTimestamp(ship);
		int secondsPassed = TimeUtil.SecondsSinceDateTime(timestampId);
		return 59 - secondsPassed;
	} 

	public void SetPlayerShip(ShipType ship) 
	{
		Ship shipData = ShipFinder.GetShip(ship);
		if (!shipData.purchased) return;

		string dataId = PrefsUtil.MiscId(MiscType.PLAYER_SHIP_TYPE);
		int shipId = (int)ship;
		DataStorage.SaveLocalData(dataId, shipId);
	}

	public ShipType PlayerShip()
	{
		string dataId = PrefsUtil.MiscId(MiscType.PLAYER_SHIP_TYPE);
		int ship = DataStorage.GetLocalData(dataId);
		return (ShipType)ship;
	}

	public void SaveShipArmorTimestamp(ShipType ship)
	{
		string timestampId = PrefsUtil.ShipArmorTimestamp(ship);
		TimeUtil.SaveDateTime(timestampId);
	}

	public int GetResources() {
		return DataStorage.GetLocalData(Remnant.GPGSIds.leaderboard_resources_earned);
	}

	public int GetResourcesSpent() {
		return DataStorage.GetLocalData(Remnant.GPGSIds.event_resources_spent);
	}

	public int GetStars() {
		return DataStorage.GetLocalData(Remnant.GPGSIds.leaderboard_stars_earned);
	}

	public int GetTotalResources() {
		return GetResources() - GetResourcesSpent();
	}

	public void AddResourcesSpent(int amount) {
		DataStorage.IncrementEvent(Remnant.GPGSIds.event_resources_spent, (uint)amount);
	}

	public void UpdateGlobalResourceNotification(int amount) {
		try { UpdateResourcesText(amount); } catch(System.NullReferenceException e) {}
	}

	public void AddResources(uint amount) {
		int current = GetResources();
		SetResources((uint)current + amount);
		DataStorage.IncrementEvent(Remnant.GPGSIds.event_resources_earned, amount);
	}

	void SetResources(uint amount) {
		DataStorage.ReportLeaderboardScore(Remnant.GPGSIds.leaderboard_resources_earned, amount);
		UpdateGlobalResourceNotification(GetTotalResources());
	}

	public void AddStars(uint amount) {
		int current = GetStars();
		SetStars((uint)current + amount);
		DataStorage.IncrementEvent(Remnant.GPGSIds.event_stars_earned, amount);
	}

	void SetStars(uint amount) {
		DataStorage.ReportLeaderboardScore(Remnant.GPGSIds.leaderboard_stars_earned, amount);
		try { UpdateStarsText((int)amount); } catch(System.NullReferenceException e) {}
	}

	int NewShipLives(ShipType ship)
	{
		string timestampId = PrefsUtil.ShipArmorTimestamp(ship);
		int minutesPassed = TimeUtil.MinutesSinceDateTime(timestampId);
		int livesGained = minutesPassed / MINUTES_TO_SHIP_RECHARGE;
		leftOverMinutes = minutesPassed % MINUTES_TO_SHIP_RECHARGE;
		if (livesGained > 0) {//enough time passed and we need to reset the timestamp
			TimeUtil.SaveDateTime(timestampId);
		}
		return livesGained;
	}

	/// <summary>
	/// Invoked by Session Manager after confirming the player has been loaded in.
	/// </summary>
	IEnumerator ProcessTimestamps() {

		//initialization when startup begins
		Ship[] ships = new Ship[8];
		for (int ship = 1; ship < 9; ship++) {
			ShipType shipType = (ShipType)ship;
			Ship s = ShipFinder.GetShip(shipType);
			int lives = ShipLives(shipType);

			if (lives < s.armor) {

				if (ShipWasPurchased(shipType)) {
					int gained = NewShipLives(shipType);
					SetShipLives(shipType, lives + gained);
				}
			}

			ships[ship - 1] = s;
		}

		while (true) {
			for (int i = 1; i < 9; i++) {
				ShipType shipType = ships[i-1].shipType;
				Ship s = ships[i-1];
				int lives = ShipLives(shipType);

				if (lives < s.armor) {

					if (ShipWasPurchased(shipType)) {
						int gained = NewShipLives(shipType); //polling for new ship lives by checking time stamp differences
						if (gained > 0) {
							SetShipLives(shipType, lives + gained);
						}
					}
				}
			}
			yield return new WaitForSeconds(1); //refresh timestamps every 5 seconds
		}
	}
}
