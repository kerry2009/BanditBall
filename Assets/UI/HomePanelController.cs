using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomePanelController : MonoBehaviour {
	private Transform itemsListContainer;

	public GameObject selectItemFrame;

	// Use this for initialization
	void Start () {
	}

	public void OpenPanel() {
		gameObject.SetActive (true);

		CreateItems ();
	}

	private void CreateItems() {
		// print ("selectItemsContainer" + selectItemsContainer);
		itemsListContainer = gameObject.transform.Find ("SelectItemsLayout/SelectItems");

		//Transform herosConfigs = GameObject.Find ("GlobalGame/gameSettings/heros").transform;

		ItemConfig childConfig;
		GameObject sif;
		Image iconImage;
		int numChildren = Global.gameSettings.heroItems.Length;

		for (int i = 0; i < numChildren; i++) {
			childConfig = Global.gameSettings.heroItems[i];

			sif = (GameObject)Instantiate(selectItemFrame);
			sif.transform.SetParent(itemsListContainer, false);

			iconImage = sif.transform.Find("icon_placeHolder/icon").gameObject.GetComponent<Image>();
			iconImage.sprite = childConfig.shopIcon;
		}
	}

	public void ClosePanel() {
		GameObject childObject;
		int numChildren = itemsListContainer.transform.childCount;
		for (int i = 0; i < numChildren; i++) {
			childObject = itemsListContainer.transform.GetChild(i).gameObject;
			Destroy(childObject);
		}

		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	}
}
