using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PauseOnEnable))]
public class Inventory : MonoBehaviour {

	// controllers needed
	public GameController GameController;

	// children needed
	public GameObject UIListContainer;
	public Image SelectedItemPortrait;
	public Text SelectedItemText;

	// prefabs needed
	public GameObject IconPrefab;

	// list of items in inventory
	private Dictionary<Collectible, Icon> _collection;
	public Dictionary<Collectible, Icon> Collection
	{
		get { return _collection; }
	}

	void Start(){
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

	public void RemoveItem(Collectible itemToRemove){
		Destroy(_collection [itemToRemove].gameObject);
		_collection.Remove (itemToRemove);

	}

	private void DisplayStats(Collectible item){
		SelectedItemPortrait.sprite = item.Sprite;
		SelectedItemText.text = item.Description;
	}

	public void IconClicked(Collectible item){
		DisplayStats (item);
	}

	public void Exit(){
		gameObject.SetActive (false);
	}

}
