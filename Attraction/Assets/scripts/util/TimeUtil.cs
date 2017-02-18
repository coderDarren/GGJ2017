using UnityEngine;
using System.Collections;
using System;

namespace Util {

	public class TimeUtil {

		public static TimeUtil Instance;

		DateTime timeSinceLastRecharge;
		string filePath;

		static DateTime GetDateTime(string dataId) {
			string data = DataStorage.GetTimeData(dataId);
			if (data == string.Empty) return Now;
			string[] dataLine = data.Split(',');
			DateTime ret = new DateTime(int.Parse(dataLine[0]),int.Parse(dataLine[1]),int.Parse(dataLine[2]),int.Parse(dataLine[3]),int.Parse(dataLine[4]),int.Parse(dataLine[5]));
			return ret;
		}

		public static void SaveDateTime(string dataId) {
			string saveLine = Now.Year.ToString() + "," +
							  Now.Month.ToString() + "," +
							  Now.Day.ToString() + "," +
							  Now.Hour.ToString() + "," +
							  Now.Minute.ToString() + "," +
							  Now.Second.ToString();
			DataStorage.SaveTimeData(dataId, saveLine);
		}

		public static int SecondsSinceDateTime(string dataId) {
			DateTime compareDate = GetDateTime(dataId);
			return (Now - compareDate).Seconds;
		}

		public static int MinutesSinceDateTime(string dataId) {
			DateTime compareDate = GetDateTime(dataId);
			return (Now - compareDate).Minutes;		
		}

		public static int HoursSinceDateTime(string dataId) {
			DateTime compareDate = GetDateTime(dataId);
			return (Now - compareDate).Hours;
		}	

		public static int DaysSinceDateTime(string dataId) {
			DateTime compareDate = GetDateTime(dataId);
			return (Now - compareDate).Days;
		}

		static DateTime Now {
			get { return DateTime.Now; }
		}

	}
}