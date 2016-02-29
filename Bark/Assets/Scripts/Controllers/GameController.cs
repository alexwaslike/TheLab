using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// sorting layer divisions
	public int maxHeight = 100;
	public int minHeight = 0;

	// main character
	public GameObject MainCharacterObj;
	public MainCharacter MainCharacter;

	// controllers
	public SpriteController SpriteController;
    public PrefabController PrefabController;
	public CombatController CombatController;

	// UI
	public GameObject AddDogUI;
	public GameObject DogInventory;
    public HUD HUD;
    public GameObject WinScreenUI;

	// other
	public bool AllowGameplay;

	// private
	private float _timeScale;


	// Monobehavior
	void Start(){
		AllowGameplay = true;
		_timeScale = Time.timeScale;
	}


	// utility
	public void SetSortingOrder(GameObject obj)
    {
		SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer> ();
		if (spriteRenderer != null) {
			spriteRenderer.sortingOrder = maxHeight - Mathf.FloorToInt(obj.transform.position.y*4);
		} else
			Debug.LogError ("Sprite Renderer null when attempting to sort object!");
	}

	public void PauseGame(bool paused)
	{
		if (paused)
		{
			Time.timeScale = 0.0f;
			MainCharacterObj.GetComponent<CharacterMovement>().enabled = false;
			AllowGameplay = false;
		} else
		{
			Time.timeScale = _timeScale;
			if(MainCharacterObj != null)
				MainCharacterObj.GetComponent<CharacterMovement>().enabled = true;
			AllowGameplay = true;
		}
	}

	// button clicks
	public void DogClicked(Dog dog)
    {
		AddDogUI.GetComponent<AddDogUI>().SelectedDog = dog;
		AddDogUI.SetActive (true);
	}

    public void KeyPickup(GameObject WinScreenUI)
    {
        WinScreenUI.SetActive(true);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

	// world events
	public void DogDeath(Dog dog){
		MainCharacter.RemoveDogFromInventory (dog);
		HUD.RemoveDogStats(dog);
		AddDogUI.GetComponent<DogCollectionUI> ().RemoveDogPortrait (dog);
		DogInventory.GetComponent<DogCollectionUI> ().RemoveDogPortrait (dog);
		CombatController.RemoveFromCombat (dog.CombatAI);
	}

}
