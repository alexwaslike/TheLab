using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Collectible))]

public class Item : MonoBehaviour {

	public GameController GameController;
	public SpriteRenderer SpriteRenderer;
	public Collectible Collectible;

	public string Name;

	public float StatModifier = 0.01f;

	void Start(){

		Collectible.Inventory = GameController.ItemInventory.GetComponent<Inventory> ();
		GameController.SetSortingOrder (SpriteRenderer);

		Collectible.Name = Name;
		Collectible.Description = WritingDB.ItemDescriptions[Name];
	}

	void OnMouseUp(){
		GameController.ItemClicked (this);
	}

    public virtual void ActivateItem()
    {
        Debug.Log("ActivateItem not implemented");
    }

}
