using UnityEngine;
using System.Collections;

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

    private float _speed;
    public float Speed
    {
        get { return _speed; }
    }

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
        _speed = (int)MovementSpeed;

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

            if (xMovement > 0)
                Animator.SetInteger("xMovement", 1);
            else if (xMovement < 0)
                Animator.SetInteger("xMovement", -1);
            else
                Animator.SetInteger("xMovement", 0);

            if (yMovement > 0)
                Animator.SetInteger("yMovement", 1);
            else if (yMovement < 0)
                Animator.SetInteger("yMovement", -1);
            else
                Animator.SetInteger("yMovement", 0);

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
            if (Animator != null) Animator.Play(AnimSouth, 0);
			break;
		case State.InInventory:
			gameObject.SetActive (false);
			_state = State.InInventory;
			break;
		}
    }

    public void Move(float x, float y)
    {
        if(Mathf.Abs(y) > 0.001)
            transform.Translate(new Vector3(x* 0.5f, y* 0.5f, 0));
        else
            transform.Translate(x, y, 0);
    }

}
