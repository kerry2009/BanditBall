using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSpawner : RangeSpawner {
	public Transform backContainer;
	public Transform frontContainer;

	// Use this for initialization
	void Start () {
	}

	protected override void OnInitObject (MovableGameObject movable) {
		movable.moveXSpeed = -0.01f;
	}

	protected override void OnSetParent (MovableGameObject movable, Vector3 mp) {
		if (Random.Range(0, 100) < 10) {
			movable.transform.SetParent(frontContainer, false);
			mp.z = frontContainer.position.z;
		} else {
			movable.transform.SetParent(backContainer, false);
			mp.z = backContainer.position.z;
		}

		Transform cTrans = movable.transform;
		cTrans.position = mp;
	}

}
