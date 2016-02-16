using UnityEngine;
using System.Collections.Generic;

public class DogCollectionUI : MonoBehaviour {

    public GameController GameController;
	public Dog selectedDog;

	public void AddDog()
    {
        GameController.MainCharacter.AddDogToInventory(selectedDog);
        GameController.HUD.AddNewDogStats(selectedDog);
        selectedDog.Creature.ChangeState(State.Follow);
        gameObject.SetActive(false);
	}

}
