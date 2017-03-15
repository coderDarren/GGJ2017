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
	public delegate void ThrusterHUDDelegate(float curr, float max);
	public static event ThrusterHUDDelegate UpdateThrusterHUD;
	public delegate void LifeFlashDelegate();
	public static event LifeFlashDelegate OnFlashLifeIcon;

	public float thrustPower = 1;
	public float thrustAccel = 0.001f;
    public float accel = 0.01f;
    public float coast = 1;
    public ExplosionParticles explosion;
    public bool thrustersEngage;

	public int lives = 5;
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

    Ship ship;

    public bool paused { get; private set; }

    void Start() 
	{
		startPos.position = this.transform.position;
		initialRot = this.transform.rotation;
		startTime = Time.time;
		state = State.WAIT;
		ship = ShipFinder.GetShip(ProgressManager.Instance.PlayerShip());
		coast = ship.coast;
		thrustPower = ship.thrustPower;
		thrustAccel = ship.thrustAccel;
		accel = ship.acceleration;
		GetComponent<SpriteRenderer>().sprite = ship.sprite;
	}

	/// <summary>
	/// Entry to gameplay
	/// Triggered by EnviroFader when the scene has finished fading in.
	/// </summary>
	IEnumerator WaitToLaunch()
	{
		/*if (!TutorialManager.Instance.TutorialIsComplete(TutorialType.THRUST_SHIP)) {
			yield return new WaitForSeconds(1);
		}*/

		/*TutorialManager.Instance.TryLaunchTutorial(TutorialType.THRUST_SHIP);
		while (!TutorialManager.Instance.TutorialIsComplete(TutorialType.THRUST_SHIP)) {
			yield return null;
		}*/
		
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

    	if (paused) {
    		return;
    	}

    	try {UpdateThrusterHUD(thrust, thrustPower);} catch (System.NullReferenceException e) {}

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

	        if (thrustersEngage) {
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
    	}

    	if (Input.GetKeyDown(KeyCode.Space)) {
			this.transform.position = startPos.position;
			this.transform.rotation = initialRot;
			state = State.ACCEL;
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
    	vel += thrust;
    	vel = Mathf.Clamp(vel, 0, coast + thrustPower);
    	transform.position += vel * transform.up * Time.fixedDeltaTime;
    }

	public void ReduceLives()
	{
		OnThrustersDisengage();
		OnFlashLifeIcon();
		lives --;
		//ProgressManager.Instance.SetShipLives(ship.shipType, ship.lives - 1);
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


		PageManager.Instance.TurnOffPage(PageType.PLAY, PageType.LEVEL_WIN);
		StartCoroutine("WaitToConfigureWinScreen");
	}

	IEnumerator WaitToConfigureWinScreen()
	{
		while (PageManager.Instance.PageIsExiting(PageType.PLAY))
		{
			yield return null;
		}

		LevelWinPage winPage = GameObject.FindObjectOfType<LevelWinPage>();
		winPage.ConfigurePage(LevelLoader.Instance.targetInfo.galaxy, LevelLoader.Instance.targetInfo.level, lives);
	}

	public void Pause() {
    	paused = true;
    }

    public void Unpause() {
    	paused = false;
    }
}