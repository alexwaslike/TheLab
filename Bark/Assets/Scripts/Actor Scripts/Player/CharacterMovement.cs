using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public float Speed = 0.1f;

    public Animator Animator;
    
	void Update()
    {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
        
        if (Mathf.Abs(vertical) > 0.001)
		    transform.Translate(new Vector3(horizontal, vertical, 0) * Speed * 0.6f * Time.deltaTime );
        else
            transform.Translate(new Vector3(horizontal, vertical, 0) * Speed * Time.deltaTime);

        if (horizontal > 0) {
            Animator.SetBool("isMoving", true);
            Animator.SetBool("facingRight", true);
        }
        else if (horizontal < 0) {
            Animator.SetBool("isMoving", true);
            Animator.SetBool("facingRight", false);
        }
        else if (vertical > 0) {
            Animator.SetBool("isMoving", true);
            Animator.SetBool("facingUp", true);
        }
        else if (vertical < 0) {
            Animator.SetBool("isMoving", true);
            Animator.SetBool("facingUp", false);
        }
        else {
            Animator.SetBool("isMoving", false);
        }
            

    }
}
