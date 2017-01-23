using UnityEngine;
using System.Collections;
using Menu;
using Types;

public class ShipController : MonoBehaviour {

	public delegate void LifeDelegate(int lives);
	public static event LifeDelegate OnLivesChanged;
	public delegate void CountdownDelegate(int time);
	public static event CountdownDelegate OnCountdown;

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

	public enum State {ACCEL, DECCEL, THRUST, RESET, WIN, WAIT, }
    public State state;
    State prevState;

    float vel;
    float thrust;
    public bool dying;

    void Start() 
	{
		startPos.position = this.transform.position;
		initialRot = this.transform.rotation;
		startTime = Time.time;
		state = State.WAIT;
		StartCoroutine("WaitToLaunch");
	}

	IEnumerator WaitToLaunch()
	{
		TutorialManager.Instance.StartTutorial(TutorialType.HOW_TO_PLAY);
		int status = TutorialManager.Instance.GetStatus(TutorialType.HOW_TO_PLAY);
		while (status == 0) {
			status = TutorialManager.Instance.GetStatus(TutorialType.HOW_TO_PLAY);
			yield return null;
		}

		int countdown = 5;
		while (countdown >= 0) {
			OnCountdown(countdown);
			countdown--;
			yield return new WaitForSeconds(1);
		}
		state = State.ACCEL;
	}

    void Update () {        
        switch (state) {
			case State.WAIT: Wait(); break;
        	case State.ACCEL: Accelerate(); break;
        	case State.DECCEL: Deccelerate(); break;
        	case State.THRUST: Thrust(); break;
			case State.RESET: MoveToStart(); break;	
			case State.WIN: OrbitPlanet(); break;
        }

        if (state != State.WAIT && state != State.RESET && lives > 0) {

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
    	vel *= dying ? 0 : 1;
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
    	vel *= dying ? 0 : 1;
    	transform.position += vel * transform.up * Time.deltaTime;
    }

    void Thrust()
    {
    	thrust += thrustAccel;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel += thrust;
    	vel = Mathf.Clamp(vel, 0, power + thrust);
    	vel *= dying ? 0 : 1;
    	transform.position += vel * transform.up * Time.deltaTime;
    }

	public void ReduceLives()
	{
		lives --;
		if (lives >= 1) {
			OnLivesChanged(lives);
			ReturnToBeginning();
		}
		else if (lives == 0) {
			transform.position = resetPos.position;
			transform.rotation = resetPos.rotation;
			StartCoroutine("HandleLoss");
		}
	}

	public void ReturnToBeginning() 
	{
		transform.position = resetPos.position;
		transform.rotation = resetPos.rotation;
		transform.localScale = Vector3.one * 0.15f;
		state = State.RESET;
		dying = false;
	}

	void MoveToStart()
	{
		float totalDist = Vector3.Distance(resetPos.position, startPos.position);
		float currDist = Vector3.Distance(transform.position, startPos.position);
		transform.position = Vector3.Lerp(transform.position, startPos.position, (totalDist - currDist + 0.01f) * resetSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, startPos.position) < 0.015f){
			vel = 0;
			thrust = 0;
			state = State.ACCEL;
		}
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
			StartCoroutine("HandleWin");
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

	IEnumerator HandleLoss()
	{
		yield return new WaitForSeconds(1);

		PageManager.Instance.TurnOffPage(PageType.PLAY, PageType.LEVEL_LOSE);
	}

	IEnumerator HandleWin()
	{
		yield return new WaitForSeconds(1);

		int status = ProgressManager.Instance.GetStatus(LevelLoader.Instance.targetInfo.galaxy, LevelLoader.Instance.targetInfo.level);
		int currStars = 0;
		switch (status)
		{
			case 2: currStars = 1; break;
			case 3: currStars = 2; break;
			case 4: currStars = 3; break;
			default: currStars = 0; break;
		}

		int sessionStars = lives >= 3 ? 3 : lives;

		if (sessionStars > currStars) {
			ProgressManager.Instance.SetProgress(LevelLoader.Instance.targetInfo.galaxy, LevelLoader.Instance.targetInfo.level, sessionStars + 1);
		}

		PageManager.Instance.TurnOffPage(PageType.PLAY, PageType.LEVEL_WIN);
		StartCoroutine("WaitToConfigureWinScreen", sessionStars);
	}

	IEnumerator WaitToConfigureWinScreen(int sessionStars)
	{
		while (PageManager.Instance.PageIsExiting(PageType.PLAY))
		{
			yield return null;
		}

		LevelWinPage winPage = GameObject.FindObjectOfType<LevelWinPage>();
		winPage.ConfigurePage(LevelLoader.Instance.targetInfo.galaxy, LevelLoader.Instance.targetInfo.level, sessionStars);
	}
}
