﻿using UnityEngine;
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

	void Awake(){

		Collectible.Inventory = GameController.ItemInventory.GetComponent<Inventory> ();
		GameController.SetSortingOrder (gameObject);

		Collectible.Name = Name;
		Collectible.Description = WritingDB.ItemDescriptions[Name];
		Collectible.Sprite = SpriteRenderer.sprite;
	}

	void OnMouseUp(){
		GameController.ItemClicked (this);
	}

}
