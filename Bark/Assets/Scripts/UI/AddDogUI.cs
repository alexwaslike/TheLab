using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(DogCollectionUI))]
[RequireComponent(typeof(PauseOnEnable))]
public class AddDogUI : MonoBehaviour {

	public GameController GameController;
	public Dog SelectedDog;
	public GameObject DogPortrait;
	public DogCollectionUI DogCollectionUI;

	void OnEnable(){
		DogPortrait.GetComponent<DogPortrait>().Dog = SelectedDog;
		DogPortrait.GetComponent<DogPortrait>().DogCollectionUI = DogCollectionUI;

		DogCollectionUI.DisplayStats(SelectedDog);
	}

	public void AddDog()
	{
		GameController.MainCharacter.AddDogToInventory(SelectedDog);
		GameController.HUD.AddNewDogStats(SelectedDog);
		DogCollectionUI.AddNewDogPortrait (SelectedDog);
		GameController.DogInventory.GetComponent<DogCollectionUI>().AddNewDogPortrait (SelectedDog);

		SelectedDog.Creature.ChangeState(State.Follow);

		gameObject.SetActive(false);
	}

	public void Exit(){
		gameObject.SetActive(false);
	}
}
