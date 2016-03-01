using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour {

	public Inventory Inventory;
	public Collectible Collectible;
	public Image Image;

	void Start () {
		Image.sprite = Collectible.Sprite;
		GetComponent<Button> ().onClick.AddListener (delegate { this.IconClicked (); });
	}

	public void IconClicked(){
		Inventory.IconClicked (Collectible);
	}
}
