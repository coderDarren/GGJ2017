using UnityEngine;
using System.Collections;
using Types;
using Remnant;

public class GPGSUtil {

	public static string GetIdDecrypted(string id) {
		switch (id) {
			case "CgkI6Ya_9ZgaEAIQgAE": return "event_galaxy_07_level_02_stars";
			case "CgkI6Ya_9ZgaEAIQIA": return "event_galaxy_06_level_02_attempts  ";
			case "CgkI6Ya_9ZgaEAIQCA": return "event_galaxy_01_level_03_attempts  ";
			case "CgkI6Ya_9ZgaEAIQIw": return "event_galaxy_06_level_05_attempts  ";
			case "CgkI6Ya_9ZgaEAIQVA": return "event_galaxy_08_level_04_wins      ";
			case "CgkI6Ya_9ZgaEAIQcQ": return "event_galaxy_04_level_02_stars     ";
			case "CgkI6Ya_9ZgaEAIQBw": return "event_galaxy_01_level_02_attempts  ";
			case "CgkI6Ya_9ZgaEAIQXg": return "achievement_perfectionist          ";
			case "CgkI6Ya_9ZgaEAIQcw": return "event_galaxy_04_level_04_stars     ";
			case "CgkI6Ya_9ZgaEAIQdg": return "event_galaxy_05_level_02_stars     ";
			case "CgkI6Ya_9ZgaEAIQGg": return "event_galaxy_05_level_01_attempts  ";
			case "CgkI6Ya_9ZgaEAIQOg": return "event_galaxy_03_level_03_wins      ";
			case "CgkI6Ya_9ZgaEAIQHQ": return "event_galaxy_05_level_04_attempts  ";
			case "CgkI6Ya_9ZgaEAIQLg": return "event_galaxy_01_level_01_wins      ";
			case "CgkI6Ya_9ZgaEAIQFQ": return "event_galaxy_04_level_01_attempts  ";
			case "CgkI6Ya_9ZgaEAIQSQ": return "event_galaxy_06_level_03_wins      ";
			case "CgkI6Ya_9ZgaEAIQPQ": return "event_galaxy_04_level_01_wins      ";
			case "CgkI6Ya_9ZgaEAIQWQ": return "achievement_experienced_pioneer    ";
			case "CgkI6Ya_9ZgaEAIQYg": return "event_galaxy_01_level_02_stars     ";
			case "CgkI6Ya_9ZgaEAIQTA": return "event_galaxy_07_level_01_wins      ";
			case "CgkI6Ya_9ZgaEAIQGw": return "event_galaxy_05_level_02_attempts  ";
			case "CgkI6Ya_9ZgaEAIQWw": return "achievement_multiplanetary_linguist";
			case "CgkI6Ya_9ZgaEAIQaQ": return "event_galaxy_02_level_04_stars     ";
			case "CgkI6Ya_9ZgaEAIQggE": return "event_galaxy_07_level_04_stars";
			case "CgkI6Ya_9ZgaEAIQMQ": return "event_galaxy_01_level_04_wins      ";
			case "CgkI6Ya_9ZgaEAIQhgE": return "event_galaxy_08_level_03_stars";

			case "CgkI6Ya_9ZgaEAIQZg": return "event_galaxy_02_level_01_stars      ";
			case "CgkI6Ya_9ZgaEAIQKg": return "event_galaxy_08_level_02_attempts   ";
			case "CgkI6Ya_9ZgaEAIQEg": return "event_galaxy_03_level_03_attempts   ";
			case "CgkI6Ya_9ZgaEAIQLQ": return "event_galaxy_08_level_05_attempts   ";
			case "CgkI6Ya_9ZgaEAIQcA": return "event_galaxy_04_level_01_stars      ";
			case "CgkI6Ya_9ZgaEAIQdA": return "event_galaxy_04_level_05_stars      ";
			case "CgkI6Ya_9ZgaEAIQTw": return "event_galaxy_07_level_04_wins       ";
			case "CgkI6Ya_9ZgaEAIQQw": return "event_galaxy_05_level_02_wins       ";
			case "CgkI6Ya_9ZgaEAIQUw": return "event_galaxy_08_level_03_wins       ";
			case "CgkI6Ya_9ZgaEAIQeg": return "event_galaxy_06_level_01_stars      ";
			case "CgkI6Ya_9ZgaEAIQcg": return "event_galaxy_04_level_03_stars      ";
			case "CgkI6Ya_9ZgaEAIQVQ": return "event_galaxy_08_level_05_wins       ";
			case "CgkI6Ya_9ZgaEAIQUg": return "event_galaxy_08_level_02_wins       ";
			case "CgkI6Ya_9ZgaEAIQeQ": return "event_galaxy_05_level_05_stars      ";
			case "CgkI6Ya_9ZgaEAIQDA": return "event_galaxy_02_level_02_attempts   ";
			case "CgkI6Ya_9ZgaEAIQJw": return "event_galaxy_07_level_04_attempts   ";
			case "CgkI6Ya_9ZgaEAIQDw": return "event_galaxy_02_level_05_attempts   ";
			case "CgkI6Ya_9ZgaEAIQHw": return "event_galaxy_06_level_01_attempts   ";
			case "CgkI6Ya_9ZgaEAIQVw": return "achievement_novice_adventurer       ";
			case "CgkI6Ya_9ZgaEAIQIg": return "event_galaxy_06_level_04_attempts   ";
			case "CgkI6Ya_9ZgaEAIQCg": return "event_galaxy_01_level_05_attempts   ";
			case "CgkI6Ya_9ZgaEAIQRg": return "event_galaxy_05_level_05_wins       ";
			case "CgkI6Ya_9ZgaEAIQXA": return "achievement_renown_space_grandmaster";
			case "CgkI6Ya_9ZgaEAIQOA": return "event_galaxy_03_level_01_wins       ";
			case "CgkI6Ya_9ZgaEAIQZQ": return "event_galaxy_01_level_05_stars      ";
			case "CgkI6Ya_9ZgaEAIQNQ": return "event_galaxy_02_level_03_wins       ";
			case "CgkI6Ya_9ZgaEAIQfg": return "event_galaxy_06_level_05_stars      ";
			case "CgkI6Ya_9ZgaEAIQUA": return "event_galaxy_07_level_05_wins       ";
			case "CgkI6Ya_9ZgaEAIQNw": return "event_galaxy_02_level_05_wins       ";
			case "CgkI6Ya_9ZgaEAIQRw": return "event_galaxy_06_level_01_wins       ";
			case "CgkI6Ya_9ZgaEAIQXQ": return "achievement_space_craft_savant      ";
			case "CgkI6Ya_9ZgaEAIQag": return "event_galaxy_02_level_05_stars      ";
			case "CgkI6Ya_9ZgaEAIQAQ": return "achievement_lost_alone__scared      ";
			case "CgkI6Ya_9ZgaEAIQfw": return "event_galaxy_07_level_01_stars      ";
			case "CgkI6Ya_9ZgaEAIQHA": return "event_galaxy_05_level_03_attempts   ";
			case "CgkI6Ya_9ZgaEAIQTQ": return "event_galaxy_07_level_02_wins       ";

			case "CgkI6Ya_9ZgaEAIQYA": 	return "event_resources_earned           ";
			case "CgkI6Ya_9ZgaEAIQFw": 	return "event_galaxy_04_level_03_attempts";
			case "CgkI6Ya_9ZgaEAIQOw": 	return "event_galaxy_03_level_04_wins    ";
			case "CgkI6Ya_9ZgaEAIQLw": 	return "event_galaxy_01_level_02_wins    ";
			case "CgkI6Ya_9ZgaEAIQew": 	return "event_galaxy_06_level_02_stars   ";
			case "CgkI6Ya_9ZgaEAIQbg": 	return "event_galaxy_03_level_04_stars   ";
			case "CgkI6Ya_9ZgaEAIQgQE": return "event_galaxy_07_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQSg": 	return "event_galaxy_06_level_04_wins    ";
			case "CgkI6Ya_9ZgaEAIQPg": 	return "event_galaxy_04_level_02_wins    ";
			case "CgkI6Ya_9ZgaEAIQaw": 	return "event_galaxy_03_level_01_stars   ";
			case "CgkI6Ya_9ZgaEAIQhAE": return "event_galaxy_08_level_01_stars   ";
			case "CgkI6Ya_9ZgaEAIQBA": 	return "event_stars_earned               ";
			case "CgkI6Ya_9ZgaEAIQGQ": 	return "event_galaxy_04_level_05_attempts";
			case "CgkI6Ya_9ZgaEAIQKQ": 	return "event_galaxy_08_level_01_attempts";
			case "CgkI6Ya_9ZgaEAIQEQ": 	return "event_galaxy_03_level_02_attempts";
			case "CgkI6Ya_9ZgaEAIQLA": 	return "event_galaxy_08_level_04_attempts";
			case "CgkI6Ya_9ZgaEAIQMg": 	return "event_galaxy_01_level_05_wins    ";
			case "CgkI6Ya_9ZgaEAIQJA": 	return "event_galaxy_07_level_01_attempts";
			case "CgkI6Ya_9ZgaEAIQdw": 	return "event_galaxy_05_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQQQ": 	return "event_galaxy_04_level_05_wins    ";
			case "CgkI6Ya_9ZgaEAIQMw": 	return "event_galaxy_02_level_01_wins    ";
			case "CgkI6Ya_9ZgaEAIQRA": 	return "event_galaxy_05_level_03_wins    ";
			case "CgkI6Ya_9ZgaEAIQXw": 	return "leaderboard_resources_earned     ";
			case "CgkI6Ya_9ZgaEAIQSA": 	return "event_galaxy_06_level_02_wins    ";
			case "CgkI6Ya_9ZgaEAIQgwE": return "event_galaxy_07_level_05_stars   ";
			case "CgkI6Ya_9ZgaEAIQCw": 	return "event_galaxy_02_level_01_attempts";
			case "CgkI6Ya_9ZgaEAIQJg": 	return "event_galaxy_07_level_03_attempts";
			case "CgkI6Ya_9ZgaEAIQDg": 	return "event_galaxy_02_level_04_attempts";
			case "CgkI6Ya_9ZgaEAIQYw": 	return "event_galaxy_01_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQBg": 	return "event_galaxy_01_level_01_attempts";
			case "CgkI6Ya_9ZgaEAIQIQ": 	return "event_galaxy_06_level_03_attempts";
			case "CgkI6Ya_9ZgaEAIQCQ": 	return "event_galaxy_01_level_04_attempts";
			case "CgkI6Ya_9ZgaEAIQDQ": 	return "event_galaxy_02_level_03_attempts";
			case "CgkI6Ya_9ZgaEAIQNg": 	return "event_galaxy_02_level_04_wins    ";
			case "CgkI6Ya_9ZgaEAIQbw": 	return "event_galaxy_03_level_05_stars   ";
			case "CgkI6Ya_9ZgaEAIQOQ": 	return "event_galaxy_03_level_02_wins    ";
			case "CgkI6Ya_9ZgaEAIQbA": 	return "event_galaxy_03_level_02_stars   ";
			case "CgkI6Ya_9ZgaEAIQAg": 	return "leaderboard_stars_earned         ";
			case "CgkI6Ya_9ZgaEAIQhwE": return "event_galaxy_08_level_04_stars   ";
			case "CgkI6Ya_9ZgaEAIQWA": 	return "achievement_trained_navigator    ";
			case "CgkI6Ya_9ZgaEAIQHg": 	return "event_galaxy_05_level_05_attempts";
			case "CgkI6Ya_9ZgaEAIQFg": 	return "event_galaxy_04_level_02_attempts";
			case "CgkI6Ya_9ZgaEAIQfQ": 	return "event_galaxy_06_level_04_stars   ";
			case "CgkI6Ya_9ZgaEAIQeA": 	return "event_galaxy_05_level_04_stars   ";
			case "CgkI6Ya_9ZgaEAIQZA": 	return "event_galaxy_01_level_04_stars   ";

			case "CgkI6Ya_9ZgaEAIQPA":  return "event_galaxy_03_level_05_wins    ";
			case "CgkI6Ya_9ZgaEAIQMA":  return "event_galaxy_01_level_03_wins    ";
			case "CgkI6Ya_9ZgaEAIQdQ":  return "event_galaxy_05_level_01_stars   ";
			case "CgkI6Ya_9ZgaEAIQAw":  return "event_in_game_currency           ";
			case "CgkI6Ya_9ZgaEAIQSw":  return "event_galaxy_06_level_05_wins    ";
			case "CgkI6Ya_9ZgaEAIQPw":  return "event_galaxy_04_level_03_wins    ";
			case "CgkI6Ya_9ZgaEAIQGA":  return "event_galaxy_04_level_04_attempts";
			case "CgkI6Ya_9ZgaEAIQVg":  return "achievement_curious_explorer     ";
			case "CgkI6Ya_9ZgaEAIQfA":  return "event_galaxy_06_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQEA":  return "event_galaxy_03_level_01_attempts";
			case "CgkI6Ya_9ZgaEAIQKw":  return "event_galaxy_08_level_03_attempts";
			case "CgkI6Ya_9ZgaEAIQEw":  return "event_galaxy_03_level_04_attempts";
			case "CgkI6Ya_9ZgaEAIQQg":  return "event_galaxy_05_level_01_wins    ";
			case "CgkI6Ya_9ZgaEAIQbQ":  return "event_galaxy_03_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQZw":  return "event_galaxy_02_level_02_stars   ";
			case "CgkI6Ya_9ZgaEAIQFA":  return "event_galaxy_03_level_05_attempts";
			case "CgkI6Ya_9ZgaEAIQYQ":  return "event_galaxy_01_level_01_stars   ";
			case "CgkI6Ya_9ZgaEAIQUQ":  return "event_galaxy_08_level_01_wins    ";
			case "CgkI6Ya_9ZgaEAIQBQ":  return "event_ships_purchased            ";
			case "CgkI6Ya_9ZgaEAIQiAE": return "event_galaxy_08_level_05_stars   ";
			case "CgkI6Ya_9ZgaEAIQaA":  return "event_galaxy_02_level_03_stars   ";
			case "CgkI6Ya_9ZgaEAIQQA":  return "event_galaxy_04_level_04_wins    ";
			case "CgkI6Ya_9ZgaEAIQWg":  return "achievement_skilled_space_captain";
			case "CgkI6Ya_9ZgaEAIQhQE": return "event_galaxy_08_level_02_stars   ";
			case "CgkI6Ya_9ZgaEAIQNA":  return "event_galaxy_02_level_02_wins    ";
			case "CgkI6Ya_9ZgaEAIQJQ":  return "event_galaxy_07_level_02_attempts";
			case "CgkI6Ya_9ZgaEAIQRQ":  return "event_galaxy_05_level_04_wins    ";
			case "CgkI6Ya_9ZgaEAIQKA":  return "event_galaxy_07_level_05_attempts";
			case "CgkI6Ya_9ZgaEAIQTg":  return "event_galaxy_07_level_03_wins    ";

			default: return "NA";
		}
	}

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

	public static string ShipId(ShipType ship) {
		switch (ship) {
			case ShipType.SHIP_01: return GPGSIds.event_ship_01_purchased;
			case ShipType.SHIP_02: return GPGSIds.event_ship_02_purchased;
			case ShipType.SHIP_03: return GPGSIds.event_ship_03_purchased;
			case ShipType.SHIP_04: return GPGSIds.event_ship_04_purchased;
			case ShipType.SHIP_05: return GPGSIds.event_ship_05_purchased;
			case ShipType.SHIP_06: return GPGSIds.event_ship_06_purchased;
			case ShipType.SHIP_07: return GPGSIds.event_ship_07_purchased;
			case ShipType.SHIP_08: return GPGSIds.event_ship_08_purchased;
		}
		return string.Empty;
	}
}
