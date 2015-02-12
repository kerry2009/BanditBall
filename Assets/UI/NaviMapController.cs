using UnityEngine;
using System.Collections;

public class NaviMapController : MonoBehaviour {
	private GameObject naviMapPanel;
	private GameObject homePanel;

	// Use this for initialization
	void Start () {
		naviMapPanel = gameObject.transform.Find ("NaviMap").gameObject;
		homePanel = gameObject.transform.Find ("HomePanel").gameObject;
		homePanel.SetActive (false);
	}

	public void OnClickArena () {
 		Application.LoadLevel ("GameArena");
	}

	public void OnClickHome () {
		HomePanelController homeController = homePanel.GetComponent<HomePanelController>();
		homeController.OpenPanel ();

		HideNaviMap ();
	}

	public void OnClickForge () {
		HomePanelController homeController = homePanel.GetComponent<HomePanelController>();
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
