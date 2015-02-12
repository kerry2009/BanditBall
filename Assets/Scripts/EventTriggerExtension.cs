using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace UnityEngine.EventSystems {

	public static class EventTriggerExtension {

		public static EventTrigger GetEventTrigger(this GameObject gameObject) {
			EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
			if (trigger == null) trigger = gameObject.AddComponent<EventTrigger>();
			return trigger;
		}
		
		public static void AddEventListener(this EventTrigger trigger, EventTriggerType eventType, UnityAction<BaseEventData> callback) {
			if (trigger == null)
				return;
			
			if (trigger.delegates == null)
				trigger.delegates = new List<EventTrigger.Entry>();
			
			EventTrigger.Entry entry = trigger.delegates.Find(e => e.eventID == eventType);
			
			if (entry == null) {
				entry = new EventTrigger.Entry();
				entry.eventID = eventType;
				entry.callback = new EventTrigger.TriggerEvent();
				trigger.delegates.Add(entry);
			}
			
			entry.callback.AddListener(callback);
		}

	}


}
