using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {
	public ItemConfig itemConfig;

	public Button clickButton;
	public Image iconImage;
	public Image iconBackground;

	private static Color normalColor = new Color (1.0f, 1.0f, 1.0f);
	private static Color grayColor = new Color (0.5f, 0.5f, 0.5f);

	public void Start() {
	}

	public void SetItemConfig(ItemConfig ic) {
		itemConfig = ic;
		iconImage.sprite = itemConfig.shopIcon;
	}

	public void OnSelected() {
		iconBackground.color = normalColor;
		iconImage.color = normalColor;
	}

	public void OnUnSelected() {
		iconBackground.color = grayColor;
		iconImage.color = grayColor;
	}

}
