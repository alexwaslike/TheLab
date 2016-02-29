using UnityEngine;
using System.Collections;

public class TallDog : DogTrait {

	private Camera _mainCamera;
	private float _originalCameraSize;
	private float _zoomedOutSize = 6.0f;
	private float _zoomSpeed = 0.08f;
	private bool zoomComplete = false;

	void Start () {

		Name = "Tall";
		Description = WritingDB.DogTraitDescriptions[Name];
		icon = Dog.Creature.GameController.SpriteController.dogTraitSprite_Tall;

	}

	void Update(){
		if (_mainCamera != null && !zoomComplete) {
			_mainCamera.orthographicSize += _zoomSpeed;
			if (_mainCamera.orthographicSize >= _zoomedOutSize)
				zoomComplete = true;
		}
	}

	public override void OnAttach(MainCharacter mainCharacter){
		_player = mainCharacter; 

		// put player on top of dog

		_mainCamera = _player.GetComponentInChildren<Camera>();
		_originalCameraSize = _mainCamera.orthographicSize;
		zoomComplete = false;

	}

	public override void OnDetach(){
		// removed player from dog

		// resest sight distance
		_mainCamera.orthographicSize = _originalCameraSize;
	}
}
