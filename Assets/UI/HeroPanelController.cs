using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroPanelController : MonoBehaviour {
	public RectTransform itemsListContainer;
	public GridLayoutGroup gridLayout;
	public ShopItem shopItemPrefab;

	// Use this for initialization
	void Start () {
	}

	public void OpenPanel() {
		gameObject.SetActive (true);

		CreateItems ();
	}

	private void CreateItems() {
		HeroItemConfig childConfig;
		ShopItem shopItem = null;
		int numChildren = Global.gameSettings.heroConfigs.Length;

		for (int i = 0; i < numChildren; i++) {
			childConfig = Global.gameSettings.heroConfigs[i];

			shopItem = (ShopItem)Instantiate(shopItemPrefab);
			shopItem.SetItemConfig(childConfig);

			shopItem.transform.SetParent(itemsListContainer, false);
		}

		if (shopItem != null) {
			itemsListContainer.sizeDelta = new Vector2(gridLayout.cellSize.x * numChildren + 10f, 160f);
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
