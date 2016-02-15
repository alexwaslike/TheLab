using UnityEngine;
using System.Collections;

public class DogCollectionUI : MonoBehaviour {

	public GameController GameController;
	public Dog selectedDog;

	public void AddDog()
    {
        GameController.MainCharacter.AddDogToInventory(selectedDog);
        selectedDog.Creature.ChangeState(State.Follow);
        gameObject.SetActive(false);
	}

}
