using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public float Speed = 0.1f;

    public Animator Animator;
    public AudioSource AudioSource;
    private float _lastSpot;
    
	void Update()
    {

        
        float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

        if (vertical != 0 || horizontal != 0) {
            float hyp = Mathf.Sqrt(transform.position.x * transform.position.x + transform.position.y * transform.position.y);
            transform.position = new Vector3(transform.position.x, transform.position.y, (hyp / 2.525f) * -0.01f);
        }

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

        if ((Mathf.Abs(vertical) > 0.001 || Mathf.Abs(horizontal) > 0.001) && !AudioSource.isPlaying) {
            AudioSource.time = _lastSpot;
            AudioSource.Play();
        } else if (Mathf.Abs(vertical) < 0.001 && Mathf.Abs(horizontal) < 0.001 && AudioSource.isPlaying) {
            _lastSpot = AudioSource.time;
            AudioSource.Stop();
        }


    }
}
