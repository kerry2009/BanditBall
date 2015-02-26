using UnityEngine;
using System.Collections;

public class EnemySpawner : RangeSpawner {


	// Use this for initialization
	void Start () {
	
	}

	protected override void OnInitObject(MovableGameObject movable) {
		movable.moveXSpeed = Random.Range (0.1f, 0.5f);
	}

}
