using UnityEngine;
using System.Collections;

public class Meter : MonoBehaviour {
	public Transform cursor;
	public Transform showPosTrans;
	public Transform hidePosTrans;

	public virtual void CursorRun() {
	}

	public virtual void StopRunCursor() {

	}

	public virtual float GetPercentage() {
		return 0f;
	}

	public void easeIn() {
		TweenLite.To (transform, showPosTrans, 0.7f, TweenRestricts.y, null, null, false);
	}
	
	public void easeOut() {
		TweenLite.To (transform, hidePosTrans, 0.7f, TweenRestricts.y, null, null, false);
	}

}
