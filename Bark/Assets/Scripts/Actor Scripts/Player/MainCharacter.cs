using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]

public class MainCharacter : MonoBehaviour {

	public GameController GameController;
	public Health Health;

	public float DogAttackMultiplier = 1.0f;
	public float DogHealthMultiplier = 1.0f;
	public float SpeedMultiplier = 1.0f;

	private List<Dog> _dogInventory;
	public List<Dog> DogInventory
    {
		get { return _dogInventory; }
	}

	protected string _attachedMethodName = "OnAttach";
	protected string _detachMethodName = "OnDetach";

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

		dog.gameObject.SendMessage (_attachedMethodName, this);
	}

	public void RemoveDogFromInventory(Dog dog){
		dog.gameObject.SendMessage (_detachMethodName, this);
		_dogInventory.Remove (dog);
	}

	public void Death(){
	}
}
