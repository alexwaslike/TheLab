using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Collectible))]

public class Item : MonoBehaviour {

	public GameController GameController;
	public SpriteRenderer SpriteRenderer;
	public Collectible Collectible;

    public GameObject Shadow;

	public string Name;

	public float StatModifier = 0.01f;
    public float InteractionRange = 5.0f;

	void Start(){

		Collectible.Inventory = GameController.ItemInventory.GetComponent<Inventory> ();
		GameController.SetSortingOrder (SpriteRenderer);

		Collectible.Name = Name;
		Collectible.Description = WritingDB.ItemDescriptions[Name];
	}

    void Update()
    {
        if (Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange)
            Shadow.SetActive(true);
        else if(Shadow.activeSelf)
            Shadow.SetActive(false);
    }

	void OnMouseUp(){
        if (Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange)
            GameController.ItemClicked (this);
	}

    public virtual void ActivateItem()
    {
        Debug.Log("ActivateItem not implemented");
    }

}
