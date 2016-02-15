using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour {

	public enum DogState{
		Box, Follow, Attack, Idle, Dead
	}
	private DogState _state;

	private string _name;
    public string Name
    {
        get { return _name; }
    }

    private string _species;
    public string Species
    {
        get { return _species; }
    }

    private float _interactionRange = 2.0f;
    public float InteractionRange
    {
        get { return _interactionRange; }
    }

    private SpriteRenderer spriteRenderer;
    private int _maxHealth = 100;

    public int Health;
    public GameObject shadow;
    public GameController GameController;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		GameController.SetSortingOrder (gameObject);
		ChangeState (DogState.Box);
        Health = _maxHealth;
	}

	void Update () {
		if (_state == DogState.Follow) {
			GameController.SetSortingOrder (gameObject);
		} else if (_state == DogState.Attack) {
			GameController.SetSortingOrder (gameObject);
		} else if (_state == DogState.Box)
        {
            if (Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange)
                shadow.SetActive(true);
            else
                shadow.SetActive(false);
        }
	}

	public void OnMouseUp(){
        if (Vector3.Distance(GameController.MainCharacter.transform.position, transform.position) <= InteractionRange)
            Clicked();
	}

	private void Clicked(){
		if (_state == DogState.Box) {
			GameController.DogClicked (this);
		}
	}

	public void ChangeState(DogState newState){

		switch (newState) {
		case DogState.Box: 
			_state = DogState.Box;
			spriteRenderer.sprite = GameController.SpriteController.dog_BoxSprite;
			break;
		case DogState.Dead: 
			_state = DogState.Dead;
			spriteRenderer.sprite = GameController.SpriteController.dog_Gravestone;
			break;
		case DogState.Follow: 
			_state = DogState.Follow;
			spriteRenderer.sprite = GameController.SpriteController.dog_StandingSprite;
			break;
		}

	}

	public void Move(float x, float y){
		transform.Translate (new Vector3(x, y, 0));
	}

}
