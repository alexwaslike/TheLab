using UnityEngine;

public class GameController : MonoBehaviour {

	public int maxHeight = 100;
	public int minHeight = 0;

	public GameObject MainCharacterObj;
	public MainCharacter MainCharacter;
	public SpriteController SpriteController;
    public PrefabController PrefabController;
	public CombatController CombatController;

	// UI
	public GameObject DogCollectionUI;
    public HUD HUD;

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

}
