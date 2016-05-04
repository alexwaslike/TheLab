using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PauseOnEnable))]
public class Inventory : MonoBehaviour {

    // default shit
    public string DefaultText = "Item descriptions display here.";
    public Sprite DefaultIcon;

	// controllers needed
	public GameController GameController;

	// children needed
	public GameObject UIListContainer;
	public Collectible SelectedItem;
	public Image SelectedItemPortrait;
	public Text SelectedItemText;
	public GameObject ActivateDogButton;
	public GameObject DeactivateDogButton;
    public GameObject UseItemButton;

	// prefabs needed
	public GameObject IconPrefab;

	// list of items in inventory
	private Dictionary<Collectible, Icon> _collection;
	public Dictionary<Collectible, Icon> Collection
	{
		get { return _collection; }
	}
	public int MaxDogsOnGround = 5;

    public AudioSource MainAudioSource;
    public AudioClip[] BackpackZipperSounds;

    void Awake(){
		_collection = new Dictionary<Collectible, Icon> ();
	}

    void OnEnable()
    {
        PlayBackpackSound();

        if (_collection.Count > 0) {
            DisplayFirstItem();
        } else {
            SelectedItem = null;
            ClearView();
        }
            

        if (UseItemButton != null)
            UseItemButton.SetActive(false);

        if(ActivateDogButton != null && DeactivateDogButton != null)
        {
            ActivateDogButton.SetActive(false);
            DeactivateDogButton.SetActive(false);
        }
    }

    public void PlayBackpackSound()
    {
        MainAudioSource.pitch = Random.Range(-1f, 2f);
        MainAudioSource.PlayOneShot(BackpackZipperSounds[(int)Random.Range(0, BackpackZipperSounds.Length)]);
        MainAudioSource.pitch = 1.0f;
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
            
            if(SelectedItem.GetComponent<Potion>() != null && GameController.MainCharacter.DogInventory.Count <= 0) {
                return;
            }

            SelectedItem.GetComponent<Item>().ActivateItem();

            if (SelectedItem.GetComponent<Note>() == null) {
                RemoveItem(SelectedItem);

                if (_collection.Count > 0)
                    DisplayFirstItem();
                else
                    SelectedItem = null;
            }
        }
        
    }

    private void DisplayFirstItem()
    {
        Collectible[] keys = new Collectible[_collection.Keys.Count];
        _collection.Keys.CopyTo(keys, 0);
        SelectedItem = keys[0];
        DisplayStats(keys[0]);
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
        if(item != null) {
            SelectedItemPortrait.sprite = item.Sprite;
            SelectedItemText.text = item.Description;
        } 

        if(GameController.AddDogUI.GetComponent<AddDogUI>().SelectedCollectible != null && SelectedItem == GameController.AddDogUI.GetComponent<AddDogUI>().SelectedCollectible) {
            ActivateDogButton.SetActive(false);
            DeactivateDogButton.SetActive(false);
        }

        if (UseItemButton != null && SelectedItem == GameController.AddItemUI.GetComponent<AddItemUI>().SelectedCollectible)
            UseItemButton.SetActive(false);
        else if (UseItemButton != null)
            UseItemButton.SetActive(true);
    
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
        SelectedItem = null;
        ClearView();
        gameObject.SetActive (false);
	}

    private void ClearView()
    {
        SelectedItemPortrait.sprite = DefaultIcon;
        SelectedItemText.text = DefaultText;
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
