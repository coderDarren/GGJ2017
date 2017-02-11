using UnityEngine;
using System.Collections;
using Types;
using Remnant;

public class GPGSUtil {

	public static string GetGalaxyAchievementId(GalaxyType galaxy) {
		switch (galaxy) {
			case GalaxyType.HOME_GALAXY: 		 return GPGSIds.achievement_lost_alone__scared;
			case GalaxyType.DAHKRI_GALAXY: 		 return GPGSIds.achievement_curious_explorer;
			case GalaxyType.KYDOR_GALAXY: 		 return GPGSIds.achievement_skilled_space_captain;
			case GalaxyType.ZAX_GALAXY: 		 return GPGSIds.achievement_trained_navigator;
			case GalaxyType.MALIX_GALAXY: 		 return GPGSIds.achievement_renown_space_grandmaster;
			case GalaxyType.XILYANTIPHOR_GALAXY: return GPGSIds.achievement_novice_adventurer;
			case GalaxyType.VIDON_GALAXY: 		 return GPGSIds.achievement_experienced_pioneer;
			case GalaxyType.RYKTAR_GALAXY: 		 return GPGSIds.achievement_multiplanetary_linguist;
			default: return string.Empty;
		}
	}

	public static string GalaxyLevelAttemptsId(GalaxyType galaxy, int level) {
		switch (galaxy) {
			case GalaxyType.HOME_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_01_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_01_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_01_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_attempts :
					   GPGSIds.event_galaxy_01_level_05_attempts;	
			case GalaxyType.DAHKRI_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_02_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_02_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_02_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_02_level_04_attempts :
					   GPGSIds.event_galaxy_02_level_05_attempts;			
			case GalaxyType.KYDOR_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_03_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_03_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_03_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_attempts :
					   GPGSIds.event_galaxy_03_level_05_attempts;			
			case GalaxyType.ZAX_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_04_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_04_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_04_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_04_level_04_attempts :
					   GPGSIds.event_galaxy_04_level_05_attempts;			
			case GalaxyType.MALIX_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_05_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_05_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_05_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_05_level_04_attempts :
					   GPGSIds.event_galaxy_05_level_05_attempts;				
			case GalaxyType.XILYANTIPHOR_GALAXY:
				return level == 1 ? GPGSIds.event_galaxy_06_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_06_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_06_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_06_level_04_attempts :
					   GPGSIds.event_galaxy_06_level_05_attempts;		
			case GalaxyType.VIDON_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_07_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_07_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_07_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_07_level_04_attempts :
					   GPGSIds.event_galaxy_07_level_05_attempts;	
			case GalaxyType.RYKTAR_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_08_level_01_attempts :
					   level == 2 ? GPGSIds.event_galaxy_08_level_02_attempts :
					   level == 3 ? GPGSIds.event_galaxy_08_level_03_attempts :
					   level == 4 ? GPGSIds.event_galaxy_08_level_04_attempts :
					   GPGSIds.event_galaxy_08_level_05_attempts;	
			default:
				return string.Empty;	
		}
	}

	public static string GalaxyLevelWinsId(GalaxyType galaxy, int level) {
		switch (galaxy) {
			case GalaxyType.HOME_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_01_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_01_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_01_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_wins :
					   GPGSIds.event_galaxy_01_level_05_wins;	
			case GalaxyType.DAHKRI_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_02_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_02_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_02_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_02_level_04_wins :
					   GPGSIds.event_galaxy_02_level_05_wins;			
			case GalaxyType.KYDOR_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_03_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_03_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_03_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_wins :
					   GPGSIds.event_galaxy_03_level_05_wins;			
			case GalaxyType.ZAX_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_04_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_04_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_04_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_04_level_04_wins :
					   GPGSIds.event_galaxy_04_level_05_wins;			
			case GalaxyType.MALIX_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_05_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_05_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_05_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_05_level_04_wins :
					   GPGSIds.event_galaxy_05_level_05_wins;				
			case GalaxyType.XILYANTIPHOR_GALAXY:
				return level == 1 ? GPGSIds.event_galaxy_06_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_06_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_06_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_06_level_04_wins :
					   GPGSIds.event_galaxy_06_level_05_wins;		
			case GalaxyType.VIDON_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_07_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_07_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_07_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_07_level_04_wins :
					   GPGSIds.event_galaxy_07_level_05_wins;	
			case GalaxyType.RYKTAR_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_08_level_01_wins :
					   level == 2 ? GPGSIds.event_galaxy_08_level_02_wins :
					   level == 3 ? GPGSIds.event_galaxy_08_level_03_wins :
					   level == 4 ? GPGSIds.event_galaxy_08_level_04_wins :
					   GPGSIds.event_galaxy_08_level_05_wins;	
			default:
				return string.Empty;	
		}
	}

	public static string GalaxyLevelStarsId(GalaxyType galaxy, int level) {
		switch (galaxy) {
			case GalaxyType.HOME_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_01_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_01_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_01_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_stars :
					   GPGSIds.event_galaxy_01_level_05_stars;	
			case GalaxyType.DAHKRI_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_02_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_02_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_02_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_02_level_04_stars :
					   GPGSIds.event_galaxy_02_level_05_stars;			
			case GalaxyType.KYDOR_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_03_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_03_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_03_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_01_level_04_stars :
					   GPGSIds.event_galaxy_03_level_05_stars;			
			case GalaxyType.ZAX_GALAXY: 	
				return level == 1 ? GPGSIds.event_galaxy_04_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_04_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_04_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_04_level_04_stars :
					   GPGSIds.event_galaxy_04_level_05_stars;			
			case GalaxyType.MALIX_GALAXY: 
				return level == 1 ? GPGSIds.event_galaxy_05_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_05_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_05_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_05_level_04_stars :
					   GPGSIds.event_galaxy_05_level_05_stars;				
			case GalaxyType.XILYANTIPHOR_GALAXY:
				return level == 1 ? GPGSIds.event_galaxy_06_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_06_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_06_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_06_level_04_stars :
					   GPGSIds.event_galaxy_06_level_05_stars;		
			case GalaxyType.VIDON_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_07_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_07_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_07_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_07_level_04_stars :
					   GPGSIds.event_galaxy_07_level_05_stars;	
			case GalaxyType.RYKTAR_GALAXY: 		
				return level == 1 ? GPGSIds.event_galaxy_08_level_01_stars :
					   level == 2 ? GPGSIds.event_galaxy_08_level_02_stars :
					   level == 3 ? GPGSIds.event_galaxy_08_level_03_stars :
					   level == 4 ? GPGSIds.event_galaxy_08_level_04_stars :
					   GPGSIds.event_galaxy_08_level_05_stars;	
			default:
				return string.Empty;	
		}
	}
}
