using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DogTraitDescription))]
public class TallDog : MonoBehaviour {

	private DogTraitDescription _description;
	public DogTraitDescription Description{
		get{ return _description; }
	}

	private MainCharacter _player;
	private Camera _mainCamera;

	private float _originalCameraSize;
	private float _zoomedOutSize = 6.0f;
	private float _zoomSpeed = 0.08f;
	private bool zoomComplete = false;

	public Dog Dog;

	void Start () {

		_description = GetComponent<DogTraitDescription> ();
		_description.Name = "Tall";
		_description.Description = WritingDB.DogTraitDescriptions[_description.Name];
		_description.icon = Dog.Creature.GameController.SpriteController.dogTraitSprite_Tall;

	}

	void Update(){
		if (_mainCamera != null && !zoomComplete) {
			_mainCamera.orthographicSize += _zoomSpeed;
			if (_mainCamera.orthographicSize >= _zoomedOutSize)
				zoomComplete = true;
		}
	}

	void OnAttach(MainCharacter mainCharacter){
		_player = mainCharacter; 

		// put player on top of dog

		_mainCamera = _player.GetComponentInChildren<Camera>();
		_originalCameraSize = _mainCamera.orthographicSize;
		zoomComplete = false;

	}

	void OnDetach(MainCharacter mainCharacter){
		// removed player from dog

		// resest sight distance
		_mainCamera.orthographicSize = _originalCameraSize;
	}
}
