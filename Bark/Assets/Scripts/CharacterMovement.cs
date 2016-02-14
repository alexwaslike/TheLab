using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour {

	private MainCharacter _mainCharacter;
	public float Speed = 0.1f;
	public float DogDistance = 0.01f;

	void Start(){
		_mainCharacter = GetComponent<MainCharacter> ();
	}

	void Update(){

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(horizontal, vertical, 0)* Speed);

		if(_mainCharacter.DogInventory.Count > 0)
			MoveDogs();

	}

	public void PositionDog(Dog dog, int index){
		float radians = ((360/_mainCharacter.DogInventory.Count)*index) * (Mathf.PI / 180.0f);
		float xLoc = transform.position.x + (Mathf.Cos (radians) * DogDistance);
		float yLoc = transform.position.y + (Mathf.Sin (radians) * DogDistance);
		dog.transform.position = new Vector3 (xLoc, yLoc, 0);
	}

	public void MoveDogs(){

		int count = 0;
		List<Dog> dogs = _mainCharacter.DogInventory;

		for (int deg = 0; deg < 360; deg += 360/dogs.Count) {

			Dog dog = dogs[count];

			float radians = deg*(Mathf.PI/180.0f);
			float xLoc = transform.position.x + (Mathf.Cos(radians) * DogDistance);
			float yLoc = transform.position.y + (Mathf.Sin(radians) * DogDistance);

			dog.Move((xLoc - dog.transform.position.x)* Speed, (yLoc - dog.transform.position.y)* Speed);

			count++;
		}

	}
}
