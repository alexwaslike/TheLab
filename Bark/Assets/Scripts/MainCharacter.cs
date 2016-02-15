using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CombatAI))]

public class MainCharacter : MonoBehaviour {

	public GameController GameController;
    public CombatAI CombatAI;

	private List<Dog> _dogInventory;
	public List<Dog> DogInventory
    {
		get { return _dogInventory; }
	}

	void Start ()
    {
        CombatAI = GetComponent<CombatAI>();
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
}
