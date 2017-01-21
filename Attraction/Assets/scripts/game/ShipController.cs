using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float power = 2;    //max speed before deccel, TBD by player
	public float thrustPower = 1;
	public float thrustAccel = 0.02f;
    public float accel = 0.01f;
    public float coast = 1;

	Vector3 startPos;
	Quaternion initialRot;


	void Awake() {
		startPos = this.transform.position;
		initialRot = this.transform.rotation;
	}

    enum State { ACCEL, DECCEL, THRUST }
    State state;
    State prevState;

    float vel;
    float thrust;

    void Update () {        
        switch (state) {
        	case State.ACCEL: Accelerate(); break;
        	case State.DECCEL: Deccelerate(); break;
        	case State.THRUST: Thrust(); break;
        }

        if (Input.GetKey(KeyCode.Mouse0)) {
        	if (state != State.THRUST) {
        		prevState = state;
        		state = State.THRUST;
        	}
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
        	if (state == State.THRUST) {
        		state = prevState;
        	}
        }
		if (Input.GetKeyDown(KeyCode.Space)) {
			this.transform.position = startPos;
			this.transform.rotation = initialRot;
		}
    }

    void Accelerate()
    {
    	thrust -= thrustAccel;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel += accel;
    	vel = Mathf.Clamp(vel, 0, power + thrust);
    	transform.position += vel * transform.up * Time.deltaTime;

    	if (vel >= power)
    		state = State.DECCEL;
    }

    void Deccelerate()
    {
    	thrust -= thrustAccel;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel -= accel;
    	vel = Mathf.Clamp(vel, coast, power + thrust);
    	transform.position += vel * transform.up * Time.deltaTime;
    }

    void Thrust()
    {
    	thrust += thrustAccel;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel += thrust;
    	vel = Mathf.Clamp(vel, 0, power + thrust);
    	transform.position += vel * transform.up * Time.deltaTime;
    }
}
