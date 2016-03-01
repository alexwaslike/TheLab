using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddItemUI : MonoBehaviour {

	public GameController GameController;
	public Collectible SelectedCollectible;
	public Icon SelectedCollectibleIcon;
	public Inventory ItemInventory;

	void OnEnable(){
		ItemInventory.IconClicked (SelectedCollectible);
		SelectedCollectibleIcon.Collectible = SelectedCollectible;
		SelectedCollectibleIcon.Inventory = ItemInventory;
		SelectedCollectibleIcon.Image.sprite = SelectedCollectible.Sprite;
	}

	public void AddItem(){
		GameController.AddItem (SelectedCollectible.GetComponent<Item>());
		Exit ();
	}

	public void Exit(){
		ItemInventory.gameObject.SetActive (false);
		gameObject.SetActive(false);
	}
}

