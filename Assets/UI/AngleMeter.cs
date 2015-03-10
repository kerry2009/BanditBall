using UnityEngine;
using System.Collections;

public class AngleMeter : Meter {
	public float angleMax;

	public override void CursorRun() {
		TweenLite.To (cursor.transform, TweenProperties.localRotation,
		              new Vector3 (0, 0, 0),
		              new Vector3 (0, 0, 60),
		              1f, TweenRestricts.z, null, null, true);
	}

	public override float GetPercentage() {
		return cursor.transform.eulerAngles.z / angleMax;
	}

	public override void StopRunCursor() {
		TweenLite.Kill (cursor.transform, TweenProperties.localRotation);
	}

}
