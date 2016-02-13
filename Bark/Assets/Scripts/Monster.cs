using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private enum MonsterState
	{
		Idle, Attack
	}
	private MonsterState _state;

	public GameController GameController;

	void Start () {
		GameController.SetSortingOrder (gameObject);
		_state = MonsterState.Idle;
	}

	void Update () {
		GameController.SetSortingOrder (gameObject);
	}

	private void Move(float x, float y){
		transform.Translate (new Vector3(x, y, 0));
	}
}
