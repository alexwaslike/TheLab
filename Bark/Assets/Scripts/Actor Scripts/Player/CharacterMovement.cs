using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour {

	public float Speed = 0.1f;
    
	void Update()
    {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(vertical) > 0.001)
		    transform.Translate(new Vector3(horizontal, vertical, 0) * Speed * (1/Mathf.Sqrt(2)) );
        else
            transform.Translate(new Vector3(horizontal, vertical, 0) * Speed);

    }
}
