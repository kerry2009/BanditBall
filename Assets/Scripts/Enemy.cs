using UnityEngine;
using System.Collections;

public class Enemy : MovableGameObject {
	public bool isDead = false;

	public BloodManager bloodManager;

	public const int BLOOD_GEN_INTERAL = 2;
	public const int BLOOD_GEN_MAX = 5;

	private int curbloodInteral = 0;
	private int curbloodNum = 0;

	void Start() {
		bloodManager = GameObject.Find ("BloodManager").GetComponent<BloodManager>();
	}

	public void OnEnemyDead() {
		isDead = true;
		curbloodNum = 0;
		curbloodInteral = BLOOD_GEN_INTERAL;
	}

	public void FixedUpdate() {
		if (isDead && bloodManager) {

			if (curbloodNum < BLOOD_GEN_MAX) {

				if (curbloodInteral == BLOOD_GEN_INTERAL) {
					curbloodInteral = 0;

					GameObject blood = bloodManager.getBlood();
					
					Vector3 bloodPos = blood.transform.position;
					bloodPos.x = transform.position.x;
					bloodPos.y = transform.position.y;
					bloodPos.z = bloodManager.bloodContainer.position.z;
					
					blood.transform.position = bloodPos;
					blood.transform.SetParent(bloodManager.bloodContainer, false);

					Animator bloodAnimator = blood.GetComponent<Animator> ();
					bloodAnimator.Play("BloodEnemyDie", 0, 0f);

					curbloodNum++;
				} else {
					curbloodInteral++;
				}

			} else {
				gameObject.SetActive (false);
			}
		}
	}

}
