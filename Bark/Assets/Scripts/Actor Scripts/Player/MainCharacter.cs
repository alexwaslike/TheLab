using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]

public class MainCharacter : MonoBehaviour {

	public GameController GameController;
	public Health Health;

	public float DogAttackMultiplier = 1.0f;
	public float DogHealthMultiplier = 1.0f;
	public float SpeedMultiplier = 1.0f;

	private List<Dog> _dogs;
	public List<Dog> DogInventory
    {
		get { return _dogs; }
	}

	private List<Item> _items;
	public List<Item> Items
	{
		get { return _items; }
	}

	protected string _attachedMethodName = "OnAttach";
	protected string _detachMethodName = "OnDetach";

	void Start ()
    {
		Health = GetComponent<Health>();
        GameController.SetSortingOrder (gameObject);
		_dogs = new List<Dog> ();
		_items = new List<Item> ();
	}
	
	void Update ()
    {
		GameController.SetSortingOrder (gameObject);
	}
		
	public void AddDogToInventory(Dog dog)
    {
		_dogs.Add (dog);
		dog.PositionDog (_dogs.IndexOf(dog));

		dog.Attached (this);
	}

	public void RemoveDogFromInventory(Dog dog){
		dog.Detached ();
		_dogs.Remove (dog);
	}

	public void AddItemToInventory(Item item){
		_items.Add (item);
	}

	public void RemoveItemFromInventory(Item item){
		_items.Remove (item);
	}

	public void Death(){
	}
}
