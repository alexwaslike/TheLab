using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]

public class MainCharacter : MonoBehaviour {

	public GameController GameController;
	public Health Health;
    public GameObject LoseUI;

	public float DogAttackMultiplier = 1.0f;
	public float DogHealthMultiplier = 1.0f;
	public float SpeedMultiplier = 1.0f;

	private List<Dog> _dogInventory;
	public List<Dog> DogInventory
    {
		get { return _dogInventory; }
	}

	private List<Item> _items;
	public List<Item> Items
	{
		get { return _items; }
	}

	private int _numActiveDogs;
	public int NumActiveDogs{
		get { return _numActiveDogs; }
	}

	void Start ()
    {
		Health = GetComponent<Health>();
        Health.MaxHealth = 40;

        GameController.SetSortingOrder(GetComponent<SpriteRenderer>());

		_dogInventory = new List<Dog> ();
		_items = new List<Item> ();
		_numActiveDogs = 0;
	}
	
	void Update ()
    {
		GameController.SetSortingOrder (GetComponent<SpriteRenderer>());
	}

	public void ActivateDog(Dog dog){
		_numActiveDogs++;

        GameController.HUD.AddNewDogStats(dog);
		dog.Creature.ChangeState (State.Follow);

        foreach (Dog doge in _dogInventory) {
            if (doge.Creature.CurrentState == State.Follow) {
                doge.PositionDog();
            }
        }
            
    }

	public void DeactivateDog(Dog dog){
		_numActiveDogs--;

        GameController.HUD.RemoveDogStats(dog);
		dog.Creature.ChangeState (State.InInventory);

        foreach (Dog doge in _dogInventory) {
            if (doge.Creature.CurrentState == State.Follow)
                doge.PositionDog();
        }    
    }
		
	public void AddDogToInventory(Dog dog)
    {
        GameController.LevelGeneration.RemoveFromGrid(dog.gameObject);

        _dogInventory.Add (dog);

        if (dog.Creature.CurrentState == State.Follow) {
            _numActiveDogs++;
            foreach (Dog doge in _dogInventory)
                doge.PositionDog();
        }
        
        dog.Attached (this);
	}

	public void RemoveDogFromInventory(Dog dog){
		_numActiveDogs--;
        _dogInventory.Remove (dog);

        foreach (Dog doge in _dogInventory)
            doge.PositionDog();
    }

	public void AddItemToInventory(Item item){
		_items.Add (item);
	}

	public void RemoveItemFromInventory(Item item){
		_items.Remove (item);
	}

	public void Death()
    {
        LoseUI.SetActive(true);
	}
}
