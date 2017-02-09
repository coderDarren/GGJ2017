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
}
