using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public ArenaGameManager gameManager;
	public Geek geek;
	public Transform hitDivLine;
	public Transform heroRunFloor;

	private Animator animator;
	private int currentState;
	private int targetState;

	private const int STATE_IDLE = 0;
	private const int STATE_RUN = 1;
	private const int STATE_AIRHIT = 2;
	private const int STATE_GROUNDHIT = 3;

	// Use this for initialization
	void Start () {
		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
		animator = GetComponent<Animator> ();
	}

	void Update () {
		Vector3 heroPos = transform.position;

		AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo (0);
		if (asi.IsName("HeroRun")) {
			heroPos.x = geek.transform.position.x;
			heroPos.y = heroRunFloor.position.y;
			currentState = STATE_RUN;
		}

		if (currentState != targetState && targetState == STATE_AIRHIT) {
			heroPos.x = geek.transform.position.x;
			heroPos.y = geek.transform.position.y;
			currentState = STATE_AIRHIT;
			targetState = STATE_RUN;
		}

		if (currentState != targetState && targetState == STATE_GROUNDHIT) {
			heroPos.x = geek.transform.position.x;
			heroPos.y = heroRunFloor.position.y;
			currentState = STATE_GROUNDHIT;
			targetState = STATE_RUN;
		}

		transform.position = heroPos;
	}

	public void hit() {

		// air hit
		if (geek.transform.position.y > hitDivLine.position.y) {
			geek.speedX = 0.8f;
			geek.speedY = -0.5f;
			playAirHit();
		} else { // ground hit
			geek.speedX += 0.5f;
			geek.speedY += 0.5f;
			playGroundHit();
		}
		
		geek.playFlyAnimation (1);
	}

	private void playAirHit() {
		targetState = STATE_AIRHIT;
		animator.Play ("HeroAirHit");
	}

	private void playGroundHit() {
		gameManager.showFlashScreen ();
		targetState = STATE_GROUNDHIT;
		animator.Play ("HeroGroundHit");
	}

	public void pauseGeekMove() {
		gameManager.followObject = null;
		geek.paused = true;
	}

	public void resumeGeekMove() {
		gameManager.showFlashScreen ();
		geek.paused = false;
	}

	public void cameraOnHero() {
		gameManager.followObject = transform;
	}

	public void cameraOnGeek() {
		gameManager.followObject = geek.transform;
	}

}
