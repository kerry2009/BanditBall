using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	private static GameSettings _gs;

	public static GameSettings gameSettings {
		get {
			if (_gs == null) {
				_gs = new GameSettings();
				_gs.initGameSettings();
			}
			return _gs;
		}
	}

}
