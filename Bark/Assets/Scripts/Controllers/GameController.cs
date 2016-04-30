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
    public GameObject Key;
    public GameObject EndBunker;

	// controllers
	public SpriteController SpriteController;
    public PrefabController PrefabController;
	public CombatController CombatController;
	public LevelGeneration LevelGeneration;
    public SoundController SoundController;

	// UI
	public GameObject DogInventory;
	public GameObject AddDogUI;
	public GameObject ItemInventory;
	public GameObject AddItemUI;
    public HUD HUD;
    public GameObject WinScreenUI;
	public GameObject PauseGray;
    public GameObject PauseUI;

	// other
	public bool AllowGameplay;

	// private
	private float _timeScale;


	// Monobehavior
	void Start(){
		AllowGameplay = true;
		_timeScale = Time.timeScale;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUI.SetActive(true);
        }
    }


	// utility
	public void SetSortingOrder(SpriteRenderer spriteRenderer)
    {
		spriteRenderer.sortingOrder = maxHeight - Mathf.FloorToInt(spriteRenderer.gameObject.transform.position.y*4);
	}

	public void PauseGame(bool paused)
	{
		if (paused) {
			Time.timeScale = 0.0f;
			MainCharacterObj.GetComponent<CharacterMovement>().enabled = false;
			AllowGameplay = false;
			PauseGray.SetActive (true);
		} else {
			Time.timeScale = _timeScale;
			if(MainCharacterObj != null)
				MainCharacterObj.GetComponent<CharacterMovement>().enabled = true;
			AllowGameplay = true;
            if (PauseGray != null) {
                PauseGray.SetActive(false);
            }
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

        if(item.UseSoundClips.Length > 0) {
            AudioClip clipToPlay = item.UseSoundClips[Random.Range(0, item.UseSoundClips.Length - 1)];
            SoundController.MainAudioSource.PlayOneShot(clipToPlay);
        }
        
	}

    public void WinGame()
    {
        WinScreenUI.GetComponent<WinUI>().PopulateText(MainCharacter.DogInventory.Count);
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

        GameObject newGrave = Instantiate(PrefabController.Gravestone, dog.transform.position, Quaternion.identity) as GameObject;
        newGrave.GetComponent<EnvironmentObject>().GameController = this;
        Destroy(dog.gameObject);
	}

	public void AddDog(Dog dog){
        
        dog.gameObject.transform.SetParent(null, false);
        dog.gameObject.transform.position = new Vector3(MainCharacterObj.transform.position.x, MainCharacterObj.transform.position.y, 0.0f);

        DogInventory.GetComponent<Inventory>().AddNewItem (dog.GetComponent<Collectible>());

		if(DogInventory.GetComponent<Inventory>().Collection.Count > DogInventory.GetComponent<Inventory>().MaxDogsOnGround)
        {
            dog.Creature.ChangeState(State.InInventory);
        }
        else
        {
            HUD.AddNewDogStats(dog);
            dog.Creature.ChangeState(State.Follow);
        }
			

        MainCharacter.AddDogToInventory (dog);

        if(CombatController.EngagedAI.Count > 0)
            CombatController.AddToCombat(dog.CombatAI);
	}

	public void AddItem(Item item){
		MainCharacter.AddItemToInventory (item);
		ItemInventory.GetComponent<Inventory> ().AddNewItem (item.GetComponent<Collectible> ());
		item.gameObject.SetActive (false);

        if (item.UseSoundClips.Length > 0) {
            AudioClip clipToPlay = item.UseSoundClips[Random.Range(0, item.UseSoundClips.Length-1)];
            SoundController.MainAudioSource.PlayOneShot(clipToPlay);
        }

        LevelGeneration.RemoveFromGrid (item.gameObject);
	}

}
