using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public float moveXSpeed;
	public bool outScreen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tranPos = transform.position;
		tranPos.x += moveXSpeed;
		transform.position = tranPos;
	}
}
