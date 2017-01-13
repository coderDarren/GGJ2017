using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPS : MonoBehaviour {

	float updateInterval = 0.5f;
 
	private float accum = 0.0f; // FPS accumulated over the interval
	private float frames = 0f; // Frames drawn over the interval
	private float timeleft; // Left time for current interval

	Text text;
	 
	void Start()
	{
	    text = GetComponent<Text>();
	    timeleft = updateInterval;  
	}
	 
	void Update()
	{
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;
	 
	    // Interval ended - update GUI text and start new interval
	    if( timeleft <= 0.0f )
	    {
	        // display two fractional digits (f2 format)
	        text.text = "FPS - " + (accum/frames).ToString("f2");
	        timeleft = updateInterval;
	        accum = 0.0f;
	        frames = 0;
	    }
	}
}
