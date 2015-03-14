using UnityEngine;
using System.Collections;

public class NaviMapController : MonoBehaviour {
	public GameObject naviMapPanel;
	public GameObject heroPanel;

	// Use this for initialization
	void Start () {
		heroPanel.SetActive (false);
	}

	public void OnClickArena () {
 		Application.LoadLevel ("GameArena");
	}

	public void OnClickHero () {
		HeroPanelController homeController = heroPanel.GetComponent<HeroPanelController>();
		homeController.OpenPanel ();

		HideNaviMap ();
	}

	public void OnClickForge () {
		HeroPanelController homeController = heroPanel.GetComponent<HeroPanelController>();
		homeController.OpenPanel ();

		HideNaviMap ();
	}

	public void ShowNaviMap () {
		naviMapPanel.SetActive (true);
	}

	public void HideNaviMap () {
		naviMapPanel.SetActive (false);
	}

}
