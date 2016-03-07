using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

    private State _state;
    public State CurrentState
    {
        get { return _state; }
    }

    private float _speed = 0.1f;
    public float Speed
    {
        get { return _speed; }
    }

	// sprites
	public Sprite Sprite_N;
	public Sprite Sprite_S;
	public Sprite Sprite_E;
	public Sprite Sprite_W;
	public Sprite Sprite_Dead;
	public Sprite Sprite_Box;

	// animations

	// other public
    public CreatureType Type;
    public string Name = "doge";
    public CombatAI CombatAI;
    public SpriteRenderer SpriteRenderer;
    public GameController GameController;

	void Start(){
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
        transform.Translate(new Vector3(x, y, 0));
    }

}
