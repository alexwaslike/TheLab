﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddDogUI : MonoBehaviour {

	public GameController GameController;
	public Collectible SelectedCollectible;
	public Icon SelectedCollectibleIcon;
	public Inventory DogInventory;

	void OnEnable(){
		DogInventory.IconClicked (SelectedCollectible);
		SelectedCollectibleIcon.Collectible = SelectedCollectible;
		SelectedCollectibleIcon.Inventory = DogInventory;
		SelectedCollectibleIcon.Image.sprite = SelectedCollectible.Sprite;
	}

	public void AddDog(){
		GameController.AddDog (SelectedCollectible.GetComponent<Dog>());
		Exit ();
	}

	public void Exit(){
		DogInventory.gameObject.SetActive (false);
		gameObject.SetActive(false);
	}
}
