using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int maxHeight = 100;
	public int minHeight = 0;

	public GameObject MainCharacterObj;
	public MainCharacter MainCharacter;
	public SpriteController SpriteController;
    public PrefabController PrefabController;

	// UI
	public GameObject DogCollectionUI;

    // combat
    public float MaxAttackersMultiplier = 1.5f;
    public int CurrentNumAttackers = 0;

	public void SetSortingOrder(GameObject obj)
    {
		SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer> ();
		if (spriteRenderer != null) {
			spriteRenderer.sortingOrder = maxHeight - Mathf.FloorToInt(obj.transform.position.y*4);
		} else
			Debug.LogError ("Sprite Renderer null when attempting to sort object!");
	}

	public void DogClicked(Dog dog)
    {
		DogCollectionUI.GetComponent<DogCollectionUI>().selectedDog = dog;
		DogCollectionUI.SetActive (true);
	}

    public bool CanAttack()
    {
        if(MainCharacter.DogInventory.Count > 0 && CurrentNumAttackers + 1 <= MaxAttackersMultiplier * MainCharacter.DogInventory.Count) {
            return true;
        }
        else if(CurrentNumAttackers + 1 <= MaxAttackersMultiplier) {
            return true;
        }
        return false;
        
    }

}
