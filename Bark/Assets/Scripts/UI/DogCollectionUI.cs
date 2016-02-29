using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PauseOnEnable))]
public class DogCollectionUI : MonoBehaviour {

	public GameController GameController;
	public GameObject DogPortraitsContainer;
	public GameObject DogPortraitPrefab;
	public Image DogDetailPortrait;

	private List<DogPortrait> _dogPortraitItems;
	public List<DogPortrait> DogStatItems
	{
		get { return _dogPortraitItems; }
	}

	void Start(){
		_dogPortraitItems = new List<DogPortrait> ();
	}

	public void AddNewDogPortrait(Dog dog)
	{
		GameObject newPortrait = Instantiate(DogPortraitPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		newPortrait.GetComponent<DogPortrait>().DogCollectionUI = this;
		newPortrait.GetComponent<DogPortrait>().Dog = dog;

		if(_dogPortraitItems == null ) _dogPortraitItems = new List<DogPortrait> ();

		_dogPortraitItems.Add(newPortrait.GetComponent<DogPortrait>());
		newPortrait.transform.SetParent(DogPortraitsContainer.transform, false);

	}

	public void RemoveDogPortrait(Dog dog)
	{
		foreach (DogPortrait dogStat in _dogPortraitItems) {
			if (dogStat.Dog == dog) {
				_dogPortraitItems.Remove(dogStat);
				Destroy(dogStat.gameObject);
				break;
			}
		}
	}

	public void DisplayStats(Dog dog){
		DogDetailPortrait.sprite = dog.Creature.Sprite_S;
	}

}
