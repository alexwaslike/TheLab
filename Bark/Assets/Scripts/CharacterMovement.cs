using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour {

	public float Speed = 0.1f;

    /*private MainCharacter _mainCharacter;

    void Start()
    {
        _mainCharacter = GetComponent<MainCharacter>();
    }*/
    
	void Update()
    {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(horizontal, vertical, 0)* Speed);

        /*for (int i = 0; i < _mainCharacter.DogInventory.Count; i++)
            _mainCharacter.DogInventory[i].PositionDog(i);*/

	}
}
