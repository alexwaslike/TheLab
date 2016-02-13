using UnityEngine;
using System.Collections.Generic;

public class MainCharacter : MonoBehaviour {

	public GameController GameController;

	private List<Dog> _dogInventory;
	public List<Dog> DogInventory{
		get { return _dogInventory; }
	}

	void Start () {
		GameController.SetSortingOrder (gameObject);
		_dogInventory = new List<Dog> ();
	}
	
	void Update () {
		GameController.SetSortingOrder (gameObject);
	}
		
	public void AddDogToInventory(Dog dog){
		_dogInventory.Add (dog);
	}
}
