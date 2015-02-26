using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MovableGameObject {
	public bool isDead = false;
	public GameObject bloodProfab;
	public Transform bloodContainer;

	private static Quaternion zeroRotation = new Quaternion();
	protected override void OnUpdate () {
		base.OnUpdate ();
		if (isDead && bloodContainer != null) {
			GameObject blood = Instantiate(bloodProfab, transform.position, zeroRotation)  as GameObject;

			Vector3 bloodPos = blood.transform.position;
			bloodPos.x = transform.position.x;
			bloodPos.y = transform.position.y;
			bloodPos.z = bloodContainer.position.z;

			blood.transform.position = bloodPos;
			blood.transform.SetParent(bloodContainer, false);
		}
	}

}
