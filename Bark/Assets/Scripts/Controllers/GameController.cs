using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// sorting layer divisions
	public int maxHeight = 500;
	public int minHeight = 0;

	// main character
	public GameObject MainCharacterObj;
	public MainCharacter MainCharacter;
	public Camera MainCamera;

	// controllers
	public SpriteController SpriteController;
    public PrefabController PrefabController;
	public CombatController CombatController;
	public LevelGeneration LevelGeneration;

	// UI
	public GameObject DogInventory;
	public GameObject AddDogUI;
	public GameObject ItemInventory;
	public GameObject AddItemUI;
    public HUD HUD;
    public GameObject WinScreenUI;
	public GameObject PauseGray;

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
	public void SetSortingOrder(SpriteRenderer spriteRenderer)
    {
		spriteRenderer.sortingOrder = maxHeight - Mathf.FloorToInt(spriteRenderer.gameObject.transform.position.y*4);
	}

	public void PauseGame(bool paused)
	{
		if (paused)
		{
			Time.timeScale = 0.0f;
			MainCharacterObj.GetComponent<CharacterMovement>().enabled = false;
			AllowGameplay = false;
			PauseGray.SetActive (true);
		} else
		{
			Time.timeScale = _timeScale;
			if(MainCharacterObj != null)
				MainCharacterObj.GetComponent<CharacterMovement>().enabled = true;
			AllowGameplay = true;
			PauseGray.SetActive (false);
		}
	}

	// object clicks
	public void DogClicked(Dog dog)
    {
		AddDogUI.GetComponent<AddDogUI> ().SelectedCollectible = dog.GetComponent<Collectible> ();
		DogInventory.SetActive (true);
		AddDogUI.SetActive (true);
	}

	public void ItemClicked(Item item){
		AddItemUI.GetComponent<AddItemUI> ().SelectedCollectible = item.GetComponent<Collectible> ();
		ItemInventory.SetActive (true);
		AddItemUI.SetActive (true);
	}

    public void KeyPickup(GameObject WinScreenUI)
    {
        WinScreenUI.SetActive(true);
    }

	// scene changes
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
		DogInventory.GetComponent<Inventory> ().RemoveItem (dog.GetComponent<Collectible> ());
		CombatController.RemoveFromCombat (dog.CombatAI);
	}

	public void AddDog(Dog dog){

        //LevelGeneration.RemoveFromGrid(dog.gameObject);
        dog.gameObject.transform.SetParent(MainCharacter.transform, false);
        dog.gameObject.transform.localPosition = new Vector3(-1, -1, 0);

        HUD.AddNewDogStats (dog);
		DogInventory.GetComponent<Inventory>().AddNewItem (dog.GetComponent<Collectible>());

		if(DogInventory.GetComponent<Inventory>().Collection.Count > DogInventory.GetComponent<Inventory>().MaxDogsOnGround)
			dog.Creature.ChangeState (State.InInventory);
		else
			dog.Creature.ChangeState (State.Follow);

        MainCharacter.AddDogToInventory (dog);
	}

	public void AddItem(Item item){
		MainCharacter.AddItemToInventory (item);
		ItemInventory.GetComponent<Inventory> ().AddNewItem (item.GetComponent<Collectible> ());
		item.gameObject.SetActive (false);

		LevelGeneration.RemoveFromGrid (item.gameObject);
	}

}
