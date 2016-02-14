using UnityEngine;
using System.Collections;

public class DogCollectionUI : MonoBehaviour {

	public GameController GameController;
	public Dog selectedDog;

	public void AddDog(){
        GameController.MainCharacter.AddDogToInventory(selectedDog);
        selectedDog.ChangeState(Dog.DogState.Follow);
        gameObject.SetActive(false);
	}

}
