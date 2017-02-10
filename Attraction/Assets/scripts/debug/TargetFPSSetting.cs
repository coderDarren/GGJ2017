using UnityEngine;
using System.Collections;

public class TargetFPSSetting : MonoBehaviour {

	void Awake() {
		Application.targetFrameRate = 60;
	}
}
