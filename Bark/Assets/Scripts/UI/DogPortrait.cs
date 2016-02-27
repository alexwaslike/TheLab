using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DogPortrait : MonoBehaviour {

	public DogCollectionUI DogCollectionUI;
	public Dog Dog;
	public Image Portrait;

	void OnEnable(){
		if(Dog != null)
			Portrait.sprite = Dog.Creature.Sprite_S;
		GetComponentInChildren<Button>().onClick.AddListener(delegate { this.PortraitClicked(); } );
	}

	void PortraitClicked(){
		DogCollectionUI.DisplayStats (Dog);
	}

}
