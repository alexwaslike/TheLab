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

	// other public
    public CreatureType Type;
    public string Name = "doge";
    public CombatAI CombatAI;
    public SpriteRenderer SpriteRenderer;
    public GameController GameController;

	void Start(){

        _rarity = (int)RarityType;
        CombatAI.Health.MaxHealth = (int)HealthType;
        _speed = (int)MovementSpeed;

		GameController.SetSortingOrder (gameObject);
	}

	void Update(){
		GameController.SetSortingOrder (gameObject);
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
			SpriteRenderer.sprite = Sprite_S;
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
            transform.Translate(new Vector3(x* (1 / Mathf.Sqrt(2)), y* (1 / Mathf.Sqrt(2)), 0));
        else
            transform.Translate(x, y, 0);
    }

}
