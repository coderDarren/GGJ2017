using UnityEngine;
using System.Collections;

namespace DebugServices {

	public enum DebugFlag
	{
		EVENT, //events
		TASK, //doing something
		STEP, //a place the compiler reached
		LOAD //load information
	}	
}
