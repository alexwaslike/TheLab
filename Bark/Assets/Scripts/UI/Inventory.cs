using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PauseOnEnable))]
public class Inventory : MonoBehaviour {

	// controllers needed
	public GameController GameController;

	// children needed
	public GameObject UIListContainer;
	public Collectible SelectedItem;
	public Image SelectedItemPortrait;
	public Text SelectedItemText;
	public GameObject ActivateDogButton;
	public GameObject DeactivateDogButton;

	// prefabs needed
	public GameObject IconPrefab;

	// list of items in inventory
	private Dictionary<Collectible, Icon> _collection;
	public Dictionary<Collectible, Icon> Collection
	{
		get { return _collection; }
	}
	public int MaxDogsOnGround = 5;

	void Awake(){
		_collection = new Dictionary<Collectible, Icon> ();
	}

	public void AddNewItem(Collectible item){
		
		if (_collection == null)
			_collection = new Dictionary<Collectible, Icon> ();

		GameObject newObject = Instantiate(IconPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

		newObject.transform.SetParent(UIListContainer.transform, false);
		newObject.GetComponent<Icon>().Inventory = this;
		newObject.GetComponent<Icon>().Collectible = item;
		_collection.Add (item.GetComponent<Collectible>(), newObject.GetComponent<Icon>());

	}

    public void UseItem()
    {
        if(SelectedItem != null) {
            SelectedItem.GetComponent<Item>().ActivateItem();

            if(SelectedItem.GetComponent<Note>() == null) {
                RemoveItem(SelectedItem);

                if (_collection.Count > 0)
                {
                    Collectible[] keys = new Collectible[_collection.Keys.Count];
                    _collection.Keys.CopyTo(keys, 0);
                    SelectedItem = keys[0];
                }
                else
                    SelectedItem = null;
            }
            
        }
        
    }

	public void ActivateDog(){
		if (GameController.MainCharacter.NumActiveDogs < MaxDogsOnGround) {
			Dog selectedDog = SelectedItem.GetComponent<Dog> ();
			if (selectedDog == null) {
				Debug.LogError ("Trying to activate non-dog??? How the f******ck did we get here?!?!?");
				return;
			}
			GameController.MainCharacter.ActivateDog (selectedDog);
            Exit();
        }
	}

	public void DeactivateDog(){
		Dog selectedDog = SelectedItem.GetComponent<Dog> ();
		if (selectedDog == null) {
			Debug.LogError ("Trying to deactivate non-dog??? How the f******ck did we get here?!?!?");
			return;
		}
		GameController.MainCharacter.DeactivateDog (selectedDog);
        Exit();
	}

	public void RemoveItem(Collectible itemToRemove){
		Destroy(_collection [itemToRemove].gameObject);
		_collection.Remove (itemToRemove);
	}

	private void DisplayStats(Collectible item){
		SelectedItem = item;
        if(SelectedItemPortrait.sprite != null)
		    SelectedItemPortrait.sprite = item.Sprite;
		SelectedItemText.text = item.Description;

		if (SelectedItem != null && SelectedItem.GetComponent<Dog>() != null && _collection.ContainsKey(SelectedItem)) {
			if (SelectedItem.gameObject.activeSelf) {
				DeactivateDogButton.SetActive (true);
				ActivateDogButton.SetActive (false);
			} else {
				DeactivateDogButton.SetActive (false);
				ActivateDogButton.SetActive (true);
			}
		}
	}

	public void IconClicked(Collectible item){
		DisplayStats (item);
	}

	public void Exit(){
		gameObject.SetActive (false);
	}

	public void SwitchToItemsTab(){
		if (GameController.DogInventory.activeSelf)
			GameController.DogInventory.SetActive (false);
		
		if (!GameController.ItemInventory.activeSelf)
			GameController.ItemInventory.SetActive (true);
	}

	public void SwitchToDogsTab(){
		if (GameController.ItemInventory.activeSelf)
			GameController.ItemInventory.SetActive (false);
		
		if (!GameController.DogInventory.activeSelf)
		GameController.DogInventory.SetActive (true);
	}

}
