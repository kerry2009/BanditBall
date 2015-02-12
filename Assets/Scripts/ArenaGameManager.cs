﻿using UnityEngine;
using System.Collections;

public class ArenaGameManager : MonoBehaviour {
	public Geek geek;
	public Hero hero;

	public Camera mainCamera;

	public GameObject cameraRestrictCorner;

	public Animator HUDAnimator;
	public Animator HeroAnimator;

	public bool gameOvered = false;
	public int gameModePhrase = 0;

	public Transform bgBack;
	public Transform bgMiddle;
	public Transform bgFront;
	public Transform floor;
	public Transform bgSky;
	public Transform bgStars;
	public Transform cloudLevel1;

	public Transform followObject;

	public float bgFrontScrollRatio = 1.0f;
	public float bgMidScrollRatio = 0.5f;
	public float bgBackScrollRatio = 0.1f;

	private float firstHitPower;
	private float firstHitAngle;

	public const int MAX_HIT_ANGLE = 60;
	public const int MIN_HIT_ANGLE = 0;
	public const float RESET_SPEED = 0.025f;

	private Vector2 lastCameraPos;

	public float gravity = 0f;

	public Transform skyVanishiStart;
	public Transform skyVanishiEnd;
	public bool resposeMouseClick;

	// Use this for initialization
	void Start () {
		resposeMouseClick = true;
		gravity = 0f;
		lastCameraPos = new Vector2 (mainCamera.transform.position.x, mainCamera.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (resposeMouseClick) {
			if (Input.GetMouseButtonUp(0)) {
				switch (gameModePhrase) {
				case 0 :
					selectedPower();
					break;
				case 1:
					selectAngleAndHit();
					break;
				default:
					onHit();
					break;
				}
			}
		}

		if (followObject) {
			cameraFollower (followObject);
		}

		Vector3 skyPos = bgSky.transform.position;
		if (skyPos.y > skyVanishiStart.position.y) {
			SpriteRenderer sr = bgSky.GetComponent<SpriteRenderer> ();
			Color skyColor = sr.color;

			if (skyPos.y > skyVanishiEnd.position.y) {
				skyColor.a = 0f;
			} else {
				float diff = (skyPos.y - skyVanishiStart.position.y) / (skyVanishiEnd.position.y - skyVanishiStart.position.y);
				skyColor.a = 1f - diff;
			}

			sr.color = skyColor;
		} else {
			SpriteRenderer sr = bgSky.GetComponent<SpriteRenderer> ();
			Color skyColor = sr.color;
			skyColor.a = 1.0f;
			sr.color = skyColor;
		}
	}

	private void cameraFollower(Transform followee) {
		Vector3 cameraPos = mainCamera.transform.position;
		cameraPos.x = followee.position.x;
		cameraPos.y = followee.position.y;
		
		float  limitBottom = cameraRestrictCorner.transform.position.y;
		if (cameraPos.y < limitBottom) {
			cameraPos.y = limitBottom;
		}
		
		float  limitLeft = cameraRestrictCorner.transform.position.x;
		if (cameraPos.x < limitLeft) {
			cameraPos.x = limitLeft;
		}

		mainCamera.transform.position = cameraPos;

		float offsetX = cameraPos.x - lastCameraPos.x;
		float offsetY = cameraPos.y - lastCameraPos.y;

		lastCameraPos.x = cameraPos.x;
		lastCameraPos.y = cameraPos.y;

		moveBackGround (bgBack, offsetX, 0, offsetX * bgBackScrollRatio, 0);
		moveBackGround (bgMiddle, offsetX, 0, offsetX * bgMidScrollRatio, 0);
		moveBackGround (bgFront, offsetX, 0, offsetX * bgFrontScrollRatio, 0);
		moveBackGround (bgSky, offsetX, offsetY, 0, 0);
		moveBackGround (bgStars, offsetX, offsetY, offsetX, offsetY);
		moveBackGround (cloudLevel1, offsetX, 0, 0, 0);
	}

	private Vector3 transPos = new Vector3 ();
	private Vector2 texturePos = new Vector2 ();
	private void moveBackGround(Transform bgTrans, float offsetX, float offsetY, float textureOffsetX, float textureOffsetY) {
		transPos.x = bgTrans.position.x + offsetX;
		transPos.y = bgTrans.position.y + offsetY;
		transPos.z = bgTrans.position.z;

		if (textureOffsetX != 0f || textureOffsetY != 0f) {
			Material mat = bgTrans.renderer.material;
			texturePos.x = mat.mainTextureOffset.x + textureOffsetX;
			texturePos.y = mat.mainTextureOffset.y + textureOffsetY;
			mat.mainTextureOffset = texturePos;
		}

		bgTrans.position = transPos;
	}

	private void selectedPower() {
		gameModePhrase = 1;
		
		AnimatorStateInfo animState = HUDAnimator.GetCurrentAnimatorStateInfo (0);
		float currentTimeScale = animState.normalizedTime - (int)(animState.normalizedTime);
		
		firstHitPower = 0f;
		if (currentTimeScale <= 0.5f) {
			firstHitPower = currentTimeScale * 2;
		} else {
			firstHitPower = (1.0f - (currentTimeScale - 0.5f));
		}

		firstHitPower = 1.0f - firstHitPower;
		HUDAnimator.SetInteger ("StartGamePhrase", gameModePhrase);
		HeroAnimator.Play ("HeroReady");
	}
	
	private void selectAngleAndHit() {
		gameModePhrase = 2;
		
		AnimatorStateInfo animState = HUDAnimator.GetCurrentAnimatorStateInfo (0);
		float currentTimeScale = animState.normalizedTime - (int)(animState.normalizedTime);
		
		firstHitAngle = 0f;
		if (currentTimeScale <= 0.5f) {
			firstHitAngle = currentTimeScale * 2;
		} else {
			firstHitAngle = (1.0f - (currentTimeScale - 0.5f));
		}
		
		HUDAnimator.SetInteger ("StartGamePhrase", gameModePhrase);
		HeroAnimator.Play ("HeroStartHit");

		hitGeekFirst ();
	}

	private void hitGeekFirst() {
		followObject = geek.transform;
		float hitAngleInRadian = (MIN_HIT_ANGLE + firstHitAngle * MAX_HIT_ANGLE) * (Mathf.PI / 180);

		gravity = 0.01f;
		geek.speedX = Mathf.Cos (hitAngleInRadian) * firstHitPower;
		geek.speedY = Mathf.Sin (hitAngleInRadian) * firstHitPower;
		// Debug.Log (geek.speedX + "`````" + geek.speedY);
		geek.playFlyAnimation (1);
	}
	
	private void onHit() {
		hero.hit ();
	}

	public void gameOver() {
		if (gameOvered == false) {
			Debug.Log("Game over!");
			gameOvered = true;
		}
	}

}
