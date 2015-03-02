using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodManager : MonoBehaviour {

	private List<GameObject> spawnedBloods;
	private List<GameObject> outScreenBloods;
	private static Quaternion zeroRotation = new Quaternion();

	public GameObject bloodProfab;
	public Transform bloodContainer;

	public Transform boundLeft;

	public BloodManager() {
		spawnedBloods = new List<GameObject> ();
		outScreenBloods = new List<GameObject> ();
	}

	void FixedUpdate() {
		int len = spawnedBloods.Count;
		
		GameObject blood;
		for (int i = len - 1; i >= 0; i--) {
			blood = spawnedBloods[i];
			if (blood.transform.position.x < boundLeft.position.x) {
				outScreenBloods.Add(blood);
				spawnedBloods.RemoveAt(i);
			}
		}
	}

	public GameObject getBlood() {
		GameObject blood;
		
		if (outScreenBloods.Count > 0) {
			blood = outScreenBloods[0];
			outScreenBloods.RemoveAt(0);
		} else {
			blood = Instantiate(bloodProfab, transform.position, zeroRotation)  as GameObject;
		}

		spawnedBloods.Add(blood);

		return blood;
	}

}
