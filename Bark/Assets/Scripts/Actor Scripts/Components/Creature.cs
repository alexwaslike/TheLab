using UnityEngine;

public class Creature : MonoBehaviour {

    private State _state;
    public State CurrentState
    {
        get { return _state; }
    }

    public CombatController.MovementSpeedType MovementSpeed;
    public CombatController.HealthType HealthType;
    public CombatController.RarityType RarityType;
    public Animator Animator;

    public float Speed;

    private int _rarity;
    public int Rarity
    {
        get { return _rarity; }
    }

    // sprites
    public Sprite Sprite_N;
	public Sprite Sprite_S;
	public Sprite Sprite_E;
	public Sprite Sprite_W;
	public Sprite Sprite_Dead;
	public Sprite Sprite_Box;

    // animation names
    public string AnimNorth;
    public string AnimSouth;
    public string AnimEast;
    public string AnimWest;

	// other public
    public CreatureType Type;
    public string Name = "doge";
    public CombatAI CombatAI;
    public SpriteRenderer SpriteRenderer;
    public GameController GameController;

    // movement detection
    private float prevX;
    private float prevY;

	void Start(){

        _rarity = (int)RarityType;
        CombatAI.Health.MaxHealth = (int)HealthType;
        Speed = (int)MovementSpeed;

		GameController.SetSortingOrder (SpriteRenderer);

        prevX = transform.position.x;
        prevY = transform.position.y;
	}

	void Update(){
		GameController.SetSortingOrder (SpriteRenderer);

        if(Animator != null)
        {
            float xMovement = transform.position.x - prevX;
            float yMovement = transform.position.y - prevY;

            if (xMovement != 0 || yMovement != 0)
            {
                Animator.SetBool("isMoving", true);

                if (xMovement > 0)
                {
                    Animator.SetBool("facingRight", true);
                    Animator.SetBool("facingLeft", false);
                }
                else if (xMovement < 0)
                {
                    Animator.SetBool("facingRight", false);
                    Animator.SetBool("facingLeft", true);
                }
                else {
                    Animator.SetBool("facingRight", false);
                    Animator.SetBool("facingLeft", false);
                }

                if (yMovement > 0)
                {
                    Animator.SetBool("facingUp", true);
                    Animator.SetBool("facingDown", false);
                }
                else if (yMovement < 0)
                {
                    Animator.SetBool("facingUp", false);
                    Animator.SetBool("facingDown", true);
                }
                else {
                    Animator.SetBool("facingUp", false);
                    Animator.SetBool("facingDown", false);
                }

            }
            else {
                Animator.SetBool("isMoving", false);
            }
                

            prevX = transform.position.x;
            prevY = transform.position.y;
        }
    }

    public void ChangeState(State newState)
    {
        switch (newState) {
		case State.Idle:
			_state = State.Idle;
			SpriteRenderer.sprite = Sprite_S;
			break;
		case State.Attack:
			_state = State.Attack;
			SpriteRenderer.sprite = Sprite_S;
			break;
		case State.Box:
			_state = State.Box;
			SpriteRenderer.sprite = Sprite_Box;
			break;
		case State.Dead:
			_state = State.Dead;
			SpriteRenderer.sprite = Sprite_Dead;
			break;
		case State.Follow:
			gameObject.SetActive (true);
			_state = State.Follow;
            if (Animator == null) SpriteRenderer.sprite = Sprite_E;
			break;
		case State.InInventory:
			gameObject.SetActive (false);
			_state = State.InInventory;
			break;
		}
    }

    public void Move(float x, float y)
    {
        if (Mathf.Abs(y) > 0.001)
            transform.Translate(x * 0.6f, y * 0.6f, 0.0f);
        else
            transform.Translate(x, y, 0.0f);
    }

}
