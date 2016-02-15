using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]

public class MainCharacter : MonoBehaviour {

	public GameController GameController;
	public Health Health;

	private List<Dog> _dogInventory;
	public List<Dog> DogInventory
    {
		get { return _dogInventory; }
	}

	void Start ()
    {
		Health = GetComponent<Health>();
        GameController.SetSortingOrder (gameObject);
		_dogInventory = new List<Dog> ();
	}
	
	void Update ()
    {
		GameController.SetSortingOrder (gameObject);
	}
		
	public void AddDogToInventory(Dog dog)
    {
		_dogInventory.Add (dog);
		dog.PositionDog (_dogInventory.IndexOf(dog));
	}

	public void RemoveDogFromInventory(Dog dog){
		_dogInventory.Remove (dog);
	}

	public void Death(){
	}
}
