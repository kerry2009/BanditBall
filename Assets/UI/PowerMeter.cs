using UnityEngine;
using System.Collections;

public class PowerMeter : Meter {
	public float maxPower;

	public override void CursorRun() {
		TweenLite.To (cursor.transform, TweenProperties.localPosition,
		              new Vector3 (cursor.transform.localPosition.x, 0, 0),
		              new Vector3 (cursor.transform.localPosition.x, maxPower, 0),
		              1f, TweenRestricts.y, null, null, true);
	}

	public override float GetPercentage() {
		Debug.Log (cursor.transform.localPosition.y / maxPower);
		return cursor.transform.localPosition.y / maxPower;
	}

	public override void StopRunCursor() {
		TweenLite.Kill (cursor.transform, TweenProperties.localPosition);
	}

}
