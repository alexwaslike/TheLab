using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float _speed = 0.1f;

	void Update(){

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(horizontal, vertical, 0)* _speed);

	}
}
