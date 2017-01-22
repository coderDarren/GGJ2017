using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float power = 2;    //max speed before deccel, TBD by player
	public float thrustPower = 1;
	public float thrustAccel = 0.02f;
    public float accel = 0.01f;
    public float coast = 1;

	int lives = 5;
	float startTime;
	float journeyLength;

	public Transform startPos;
	public Transform resetPos;
	Transform planet;
	public float resetSpeed = 2f;
	Quaternion initialRot;

	void Start() 
	{
		startPos.position = this.transform.position;
		initialRot = this.transform.rotation;
		startTime = Time.time;
	}

	enum State {ACCEL, DECCEL, THRUST, RESET, WIN, WAIT, }
    State state;
    State prevState;

    float vel;
    float thrust;

    void Update () {        
        switch (state) {
			case State.WAIT: Wait(); break;
        	case State.ACCEL: Accelerate(); break;
        	case State.DECCEL: Deccelerate(); break;
        	case State.THRUST: Thrust(); break;
			case State.RESET: MoveToStart(); break;	
			case State.WIN: OrbitPlanet(); break;
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
			this.transform.position = startPos.position;
			this.transform.rotation = initialRot;
		}
    }

	public void Wait()
	{
		transform.position = startPos.position;
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

	public void ReduceLives()
	{
		ReturnToBeginning();
		lives --;
	}

	public void ReturnToBeginning() 
	{
		transform.position = resetPos.position;
		transform.rotation = resetPos.rotation;
	}

	void MoveToStart()
	{
		float distCovered = (Time.time - startTime) * resetSpeed;
		float fracJourney = distCovered / GetJourneyLength();
		transform.position = Vector3.Lerp(resetPos.position, startPos.position, fracJourney);
		if (transform.position == startPos.position)
			state = State.ACCEL;
	}

	float GetJourneyLength()
	{
		return journeyLength = Vector3.Distance(resetPos.position, startPos.position);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Planet")) {			
			planet = col.gameObject.transform;
			state = State.WIN;
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void OrbitPlanet()
	{
//		Vector3 relativePos = (planet.position + new Vector3(0,1.5f,0)) - transform.position;
//		Quaternion rotation = Quaternion.LookRotation(relativePos);
//		Quaternion current = transform.localRotation;
//		transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
//		transform.Translate(0,0,1*Time.deltaTime);
		Vector3 vectorToTarget = planet.position - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, (resetSpeed * .75f) * Time.deltaTime);
		transform.position += resetSpeed * transform.up * Time.deltaTime;
	}
}
