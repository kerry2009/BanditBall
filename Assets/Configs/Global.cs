using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	private static GameSettings _gs;

	public static GameSettings gameSettings {
		get {
			if (_gs == null) {
				GameObject go = Resources.Load("GameConfigure") as GameObject;
				_gs = go.GetComponent<GameSettings>();
			}
			return _gs;
		}
	}

}
