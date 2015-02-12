using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudManager : MonoBehaviour {
	public Cloud[] clouds;		// Array of cloud prefabs.
	public Transform backContainer;
	public Transform frontContainer;
	public Transform[] spawnPoints;
	public Transform cloudLeftEdge;

	private float oldSpawnX = 0f;
	public float spawnXDist;

	private List<Cloud> spawnedClouds;
	private List<Cloud> outScreenClouds;

	// Use this for initialization
	void Start () {
		spawnedClouds = new List<Cloud> ();
		outScreenClouds = new List<Cloud> ();
	}
	
	// Update is called once per frame
	void Update () {
		int len, i;

		if (transform.position.x - oldSpawnX > spawnXDist) {
			oldSpawnX = transform.position.x;

			len = spawnPoints.Length - 1;
			for (i = 0; i < len; i++) {
				Spawn(spawnPoints[i], spawnPoints[i + 1]);
			}
		}

		len = spawnedClouds.Count;
		float left = cloudLeftEdge.position.x;
		Cloud cloud;
		for (i = len - 1; i >= 0; i--) {
			cloud = spawnedClouds[i];
			if (cloud.transform.position.x < left) {
				cloud.moveXSpeed = 0f;
				outScreenClouds.Add(cloud);
				spawnedClouds.RemoveAt(i);
			}
		}

	}

	private Vector3 spawnPoint = new Vector3 ();
	private void Spawn(Transform spawnBottomPoint, Transform spawnTopPoint) {
		if (clouds.Length > 0) {
			// Instantiate a random enemy.
			int enemyIndex = Random.Range(0, clouds.Length);

			spawnPoint.x = spawnBottomPoint.position.x;
			spawnPoint.y = Random.Range(spawnBottomPoint.position.y, spawnTopPoint.position.y);
			spawnPoint.z = spawnBottomPoint.position.z;

			Cloud cloud;
			if (outScreenClouds.Count > 0) {
				cloud = outScreenClouds[0];
				outScreenClouds.RemoveAt(0);
			} else {
				cloud = Instantiate(clouds[enemyIndex], spawnPoint, spawnBottomPoint.rotation) as Cloud;
			}

			if (Random.Range(0, 100) < 10) {
				cloud.transform.SetParent(frontContainer, false);
				spawnPoint.z = frontContainer.position.z;
			} else {
				cloud.transform.SetParent(backContainer, false);
				spawnPoint.z = backContainer.position.z;
			}

			Transform cTrans = cloud.transform;
			cTrans.position = spawnPoint;

			cloud.moveXSpeed = -0.01f;
			spawnedClouds.Add(cloud);
			cloud.outScreen = false;
		}
	}

}
