using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeSpawner : MonoBehaviour {
	public Enemy[] profabs;		// Array of cloud prefabs.

	private float oldSpawnX = 0f;
	public float spawnXDist;

	public Transform rangeTopPoint;
	public Transform leftEdge;
	public Transform rightEdge;
	public Transform gameObjectContainer;
	public Transform bloodContainer;

	protected List<Enemy> spawnedObjects;
	protected List<Enemy> outScreenObjects;

	public RangeSpawner() {
		spawnedObjects = new List<Enemy> ();
		outScreenObjects = new List<Enemy> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int len = spawnedObjects.Count;
		float left, right;

		if (leftEdge != null) {
			left = leftEdge.position.x;
		} else {
			left = int.MinValue;
		}

		if (rightEdge != null) {
			right = rightEdge.position.x;
		} else {
			right = int.MaxValue;
		}

		Enemy enemy;
		for (int i = len - 1; i >= 0; i--) {
			enemy = spawnedObjects[i];
			if (enemy.transform.position.x < left || enemy.transform.position.x > right) {
				//movable.moveXSpeed = 0f;
				enemy.gameObject.SetActive (false);
				outScreenObjects.Add(enemy);
				spawnedObjects.RemoveAt(i);
			}
		}

		if (transform.position.x - oldSpawnX > spawnXDist) {
			oldSpawnX = transform.position.x;
			SpawnGameObject();
		}
	}

	protected void SpawnGameObject() {
		Spawn(OnSpawnEnemy(), rangeTopPoint);
	}

	private static Quaternion zeroRotation = new Quaternion();
	protected virtual Enemy OnSpawnEnemy() {
		Enemy enemy;

		if (outScreenObjects.Count > 0) {
			enemy = outScreenObjects[0];
			outScreenObjects.RemoveAt(0);
		} else {
			// Instantiate a random enemy.
			int randIndex = Random.Range(0, profabs.Length);
			enemy = Instantiate(profabs[randIndex], p, zeroRotation)  as Enemy;
			enemy.bloodContainer = bloodContainer;
		}

		enemy.gameObject.SetActive (true);
		return enemy;
	}

	private static Vector3 p = new Vector3 ();
	protected void Spawn(Enemy enemy, Transform endTopPoint) {
		p.x = transform.position.x;
		p.y = Random.Range(transform.position.y, endTopPoint.position.y);
		p.z = transform.position.z;
		OnSetParent (enemy, p);
		OnInitObject (enemy);
		enemy.isDead = false;
		spawnedObjects.Add(enemy);
	}

	protected virtual void OnInitObject(MovableGameObject movable) {
	}

	protected virtual void OnSetParent(MovableGameObject movable, Vector3 mp) {
		mp.z = gameObjectContainer.position.z;

		movable.transform.SetParent (gameObjectContainer, false);

		Transform cTrans = movable.transform;
		cTrans.position = mp;
	}

}
