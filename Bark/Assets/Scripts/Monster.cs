using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private enum MonsterState
	{
		Idle, Attack
	}
	private MonsterState _state;

	private float _interactionRange = 2.0f;
	public float InteractionRange
	{
		get { return _interactionRange; }
	}

	private SpriteRenderer _spriteRenderer;
	private int _maxHealth = 100;
	private int _attackDamage = 1;

	public int Health;
	public GameController GameController;

	private string _name;
	public string Name
	{
		get { return _name; }
	}


	void Start () {
		GameController.SetSortingOrder (gameObject);
		_state = MonsterState.Idle;
	}

	void Update () {

		if (_state == MonsterState.Attack) {




		} else if (_state == MonsterState.Idle) {



		}

		GameController.SetSortingOrder (gameObject);
	}

	private void Move(float x, float y){
		transform.Translate (new Vector3(x, y, 0));
	}
}
