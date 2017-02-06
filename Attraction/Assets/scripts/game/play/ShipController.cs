using UnityEngine;
using System.Collections;
using Menu;
using Types;

public class ShipController : MonoBehaviour {

	public delegate void LifeDelegate(int lives);
	public static event LifeDelegate OnLivesChanged;
	public delegate void CountdownDelegate(int time);
	public static event CountdownDelegate OnCountdown;
	public delegate void ThrusterDelegate();
	public static event ThrusterDelegate OnThrustersEngage;
	public static event ThrusterDelegate OnThrustersDisengage;

	public GameObject accelButton;
	public GameObject decelButton;
	public ButtonPressAccelerate bpa;
	public ButtonPressDecelerate bpd;

	public float thrustPower = 2;
	public float thrustAccel = 0.02f;
    public float accel = 0.01f;
    public float coast = 1;
    public ExplosionParticles explosion;

	int lives = 5;
	float startTime;
	float journeyLength;

	public Transform startPos;
	public Transform resetPos;
	Transform planet;
	Quaternion initialRot;

	public enum State {ACCEL, DECCEL, THRUST, RESET, WIN, WAIT, DYING}
    public State state;

    float vel;
    float thrust;

    void Start() 
	{
		startPos.position = this.transform.position;
		initialRot = this.transform.rotation;
		startTime = Time.time;
		state = State.WAIT;

		accelButton = GameObject.FindGameObjectWithTag("AccelButton");
		decelButton = GameObject.FindGameObjectWithTag("DecelButton");
		bpa = accelButton.GetComponent<ButtonPressAccelerate>();
		bpd = decelButton.GetComponent<ButtonPressDecelerate>();
	}

	/// <summary>
	/// Entry to gameplay
	/// Triggered by EnviroFader when the scene has finished fading in.
	/// </summary>
	IEnumerator WaitToLaunch()
	{
		TutorialManager.Instance.StartTutorial(TutorialType.HOW_TO_PLAY);
		int status = TutorialManager.Instance.GetStatus(TutorialType.HOW_TO_PLAY);
		while (status == 0) {
			status = TutorialManager.Instance.GetStatus(TutorialType.HOW_TO_PLAY);
			yield return null;
		}

		int countdown = 3;
		while (countdown >= 0) {
			OnCountdown(countdown);
			countdown--;
			yield return new WaitForSeconds(1);
		}
		state = State.ACCEL;
	}

    void FixedUpdate () {        

    	if (lives == 0)
    		return;

        switch (state) {
			case State.WAIT: Wait(); break;
        	case State.ACCEL: Accelerate(); break;
        	case State.DECCEL: Deccelerate(); break;
        	case State.THRUST: Thrust(); break;
			case State.RESET: MoveToStart(); break;	
			case State.WIN: OrbitPlanet(); break;
			case State.DYING: Die(); break;
        }

        if ((state == State.ACCEL || state == State.DECCEL || state == State.THRUST) && lives > 0) {

	        if (Input.GetKey(KeyCode.Mouse0)) {
	        	if (state != State.THRUST) {
	        		state = State.THRUST;
	        		OnThrustersEngage();
	        	}
	        } else {
	        	if (state == State.THRUST) {
	        		state = State.DECCEL;
	        		OnThrustersDisengage();
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

	void Die() {
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.02f, 1.5f * Time.deltaTime);
		if (transform.localScale.magnitude < 0.05f) {
			explosion.gameObject.transform.position = transform.position;
			explosion.SpawnParticles();
			ReduceLives();
		}
	}

    void Accelerate()
    {
    	thrust -= thrustAccel * Time.fixedDeltaTime * 75;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel += accel * Time.fixedDeltaTime * 75;
    	vel = Mathf.Clamp(vel, 0, coast + thrustPower);
    	transform.position += vel * transform.up * Time.fixedDeltaTime;

    	if (vel >= coast + thrust)
    		state = State.DECCEL;
    }

    void Deccelerate()
    {
    	thrust -= thrustAccel * Time.fixedDeltaTime * 75;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel -= accel * Time.fixedDeltaTime * 75;
    	vel = Mathf.Clamp(vel, coast, coast + thrustPower);
    	transform.position += vel * transform.up * Time.fixedDeltaTime;
    }

    void Thrust()
    {
    	thrust += thrustAccel * Time.fixedDeltaTime * 75;
    	thrust = Mathf.Clamp(thrust, 0, thrustPower);
    	vel += thrust * Time.fixedDeltaTime * 75;
    	vel = Mathf.Clamp(vel, 0, coast + thrustPower);
    	transform.position += vel * transform.up * Time.fixedDeltaTime;
    }

	public void ReduceLives()
	{
		OnThrustersDisengage();
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
		vel = 0;
		thrust = 0;
		state = State.RESET;
	}


	void MoveToStart()
	{
		Deccelerate();
		if (Vector3.Distance(transform.position, startPos.position) < 0.03f){
			state = State.DECCEL;
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
			OnThrustersDisengage();
			StartCoroutine("HandleWin");
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void OrbitPlanet()
	{
		Vector3 vectorToTarget = planet.position - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, (2 * .75f) * Time.fixedDeltaTime);
		transform.position += 2 * transform.up * Time.fixedDeltaTime;
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