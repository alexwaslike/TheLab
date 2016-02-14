using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour {

	public enum DogState{
		Box, Follow, Attack, Idle, Dead
	}

	private DogState _state;
	private bool _mouseWasDown;
	private SpriteRenderer spriteRenderer;

	public GameController GameController;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		GameController.SetSortingOrder (gameObject);
		ChangeState (DogState.Box);
	}

	void Update () {
		if (_state == DogState.Follow) {
			GameController.SetSortingOrder (gameObject);
		} else if (_state == DogState.Attack) {
			GameController.SetSortingOrder (gameObject);
		}
	}

	public void OnMouseUp(){
		Clicked ();
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
