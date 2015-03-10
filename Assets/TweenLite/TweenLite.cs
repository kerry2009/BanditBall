using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate float Ease(float t, float b, float c, float d);
public delegate void CallBack();

public enum TweenProperties : short {
	position = 	1 << 0,
	localPosition = 	1 << 1,

	scale = 		1 << 2,
	localScale = 		1 << 3,

	rotation = 	1 << 4,
	localRotation = 	1 << 5,

	alpha = 		1 << 6,

	all = position | localPosition | scale | localScale | rotation | localRotation | alpha
}

public enum TweenRestricts : short {
	x = 1 << 0,
	y = 1 << 1,
	z = 1 << 2,
	all = x | y | z
}

public class TweenLite : MonoBehaviour {

	private static List<TweenLiteObject> tweenObjects;

	static TweenLite() {
		tweenObjects = new List<TweenLiteObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		int tLen = tweenObjects.Count;
		float deltaTime = Time.deltaTime;

		for (int i = tLen - 1; i >= 0; i--) {
			if (tweenObjects[i].tween(deltaTime)) {
				tweenObjects[i].kill();
				tweenObjects.RemoveAt(i);
			}
		}
	}

	public static void Kill(Transform tweener, TweenProperties property) {
		int tLen = tweenObjects.Count;
		
		for (int i = tLen - 1; i >= 0; i--) {
			if (tweenObjects[i].tweener == tweener) {
				if ((tweenObjects[i].property & property) == tweenObjects[i].property) {
					tweenObjects[i].kill();
					tweenObjects.RemoveAt(i);
				}
			}
		}
	}

	public static void To(Transform tweener, Transform toValue, float duration, TweenRestricts restrict, Ease ease, CallBack OnComplete, bool yoyo) {
		if (tweener.position != toValue.position) {
			To(tweener, TweenProperties.position, tweener.position, toValue.position, duration, restrict, ease, OnComplete, yoyo);
		}

		if (tweener.localScale != toValue.localScale) {
			To(tweener, TweenProperties.scale, tweener.localScale, toValue.localScale, duration, restrict, ease, OnComplete, yoyo);
		}

		if (tweener.rotation != toValue.rotation) {
			To(tweener, TweenProperties.rotation, new Vector3(tweener.rotation.x, tweener.rotation.y, tweener.rotation.z),
			   new Vector3(toValue.rotation.x, toValue.rotation.y, toValue.rotation.z), duration, restrict, ease, OnComplete, yoyo);
		}
	}

	public static void To(Transform tweener, TweenProperties property, Vector3 fromValue, Vector3 toValue, float duration, TweenRestricts restricts, Ease ease, CallBack OnComplete, bool yoyo) {
		TweenLiteObject tweenObject = new TweenLiteObject(tweener, property, fromValue, toValue, duration, restricts, ease);
		tweenObject.completeCallBack = OnComplete;
		tweenObject.yoyo = yoyo;
		tweenObjects.Add (tweenObject);
	}

}

class TweenLiteObject {
	private float time = 0;
	private float duration;

	private Vector3 fromValue;
	private Vector3 toValue;

	public Transform tweener;
	public TweenProperties property;

	public TweenRestricts restricts;
	public Ease tweenEase;
	public CallBack completeCallBack = null;
	public bool yoyo = false;

	public TweenLiteObject(Transform tweener, TweenProperties property, Vector3 fromValue, Vector3 toValue, float duration, TweenRestricts restricts, Ease ease) {
		this.tweener = tweener;
		this.property = property;
		this.duration = duration;
		this.restricts = restricts;

		if ((restricts & TweenRestricts.x) == 0) {
			toValue.x = fromValue.x;
		}
		
		if ((restricts & TweenRestricts.y) == 0) {
			toValue.y = fromValue.y;
		}
		
		if ((restricts & TweenRestricts.z) == 0) {
			toValue.z = fromValue.z;
		}

		this.fromValue = fromValue;
		this.toValue = toValue;

		if (ease != null) {
			tweenEase = ease;
		} else {
			tweenEase = defaultEase;
		}

		vect.Set(fromValue.x, fromValue.y, fromValue.z);
		setTweenValue ();
	}

	private Vector3 vect = new Vector3();
	public bool tween(float deltaTime) {
		if (tweener != null) {

			if (time + deltaTime >= duration) {
				time = duration;
			} else {
				time += deltaTime;

				if ((restricts & TweenRestricts.x) != 0) {
					vect.x = fromValue.x + tweenEase (time, 0, 1, duration) * (toValue.x - fromValue.x);
				}

				if ((restricts & TweenRestricts.y) != 0) {
					vect.y = fromValue.y + tweenEase (time, 0, 1, duration) * (toValue.y - fromValue.y);
				}
				
				if ((restricts & TweenRestricts.z) != 0) {
					vect.z = fromValue.z + tweenEase (time, 0, 1, duration) * (toValue.z - fromValue.z);
				}
			}

			if (time < duration) {
				setTweenValue ();
				return false;
			} else {
				vect.Set(toValue.x, toValue.y, toValue.z);
				setTweenValue ();

				if (yoyo) {
					toValue.Set(fromValue.x, fromValue.y, fromValue.z);
					fromValue.Set(vect.x, vect.y, vect.z);

					time = 0f;
					return false;
				}
			}

			if (completeCallBack != null) {
				completeCallBack();
			}
		}

		return true;
	}

	private void setTweenValue() {
		Vector3 v3;

		switch (property) {
		case TweenProperties.position:
			v3 = tweener.position;
			v3.Set(vect.x, vect.y, vect.z);
			tweener.position = v3;
			break;
		case TweenProperties.localPosition:
			v3 = tweener.localPosition;
			v3.Set(vect.x, vect.y, vect.z);
			tweener.localPosition = v3;
			break;
		case TweenProperties.scale:
			v3 = tweener.localScale;
			v3.Set(vect.x, vect.y, vect.z);
			tweener.localScale = vect;
			break;
		case TweenProperties.rotation:
			v3 = tweener.rotation.eulerAngles;
			v3.Set(vect.x, vect.y, vect.z);
			tweener.rotation = Quaternion.Euler(v3);
			break;
		case TweenProperties.localRotation:
			v3 = tweener.localRotation.eulerAngles;
			v3.Set(vect.x, vect.y, vect.z);
			tweener.localRotation = Quaternion.Euler(v3);
			break;
		}
	}

	private static float defaultEase(float t, float b, float c, float d) {
		return c * t / d + b;
	}

	/**
	 * Kill the tween
	 */
	public void kill() {
		completeCallBack = null;
		tweenEase = null;
	}

}