using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NaviMapButton : MonoBehaviour {
	public Sprite mouseOverSprite;
	private Sprite defualtSprite;

	// Use this for initialization
	void Start () {
		defualtSprite = GetComponent<Image>().sprite;

		EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
		if (trigger == null) {
			trigger = gameObject.AddComponent<EventTrigger>();
		}

		trigger.AddEventListener(EventTriggerType.PointerEnter, ScaleUp);
		trigger.AddEventListener(EventTriggerType.PointerExit, ScaleDown);
	}

	public void ScaleUp(BaseEventData evt) {
		GetComponent<Image>().sprite = mouseOverSprite;

		Vector3 localScal = gameObject.transform.localScale;
		localScal.x = 1.1f;
		localScal.y = 1.1f;
		gameObject.transform.localScale = localScal;
	}

	public void ScaleDown(BaseEventData evt) {
		GetComponent<Image>().sprite = defualtSprite;

		Vector3 localScal = gameObject.transform.localScale;
		localScal.x = 1.0f;
		localScal.y = 1.0f;
		gameObject.transform.localScale = localScal;
	}
}
