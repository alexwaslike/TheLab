using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public SpriteRenderer SpriteRenderer;
	public float StatModifier = 0.01f;
	public string Description;

	public ItemInventory ItemInventory;

	void OnEnable(){
		GetComponentInChildren<Button>().onClick.AddListener(delegate { this.Clicked(); } );
	}

	void Clicked(){
		// TODO: display item inventory
	}

}
