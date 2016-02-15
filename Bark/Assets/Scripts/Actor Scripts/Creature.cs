using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

    private string _name = "doge";
	public string Name
	{
		get { return _name; }
	}

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

    public CreatureType Type;
    public CombatAI CombatAI;
    public SpriteRenderer SpriteRenderer;
    public GameController GameController;

    public void ChangeState(State newState)
    {
		if (Type == CreatureType.Dog) {
			switch (newState) {
			case State.Attack:
				_state = State.Attack;
				break;
			case State.Box:
				_state = State.Box;
				SpriteRenderer.sprite = GameController.SpriteController.dog_BoxSprite;
				break;
			case State.Dead:
				_state = State.Dead;
				SpriteRenderer.sprite = GameController.SpriteController.dog_Gravestone;
				break;
			case State.Follow:
				_state = State.Follow;
				SpriteRenderer.sprite = GameController.SpriteController.dog_StandingSprite;
				break;
			}
		} else if (Type == CreatureType.Monster) {
			switch (newState) {
			case State.Idle:
				_state = State.Idle;
				SpriteRenderer.sprite = GameController.SpriteController.monster_IdleSprite;
				break;
			case State.Attack:
				_state = State.Attack;
				SpriteRenderer.sprite = GameController.SpriteController.monster_AttackSprite;
				break;
			case State.Dead:
				_state = State.Dead;
				SpriteRenderer.sprite = GameController.SpriteController.dog_Gravestone;
				break;
			}
		} else
			Debug.Log("Creature Type state control not implemented in Creature!");
    }

    public void Move(float x, float y)
    {
        transform.Translate(new Vector3(x, y, 0));
    }

}
