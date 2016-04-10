using UnityEngine;

public class EnvironmentObject : MonoBehaviour {

	public GameController GameController;

	void Start () {
		GameController.SetSortingOrder (GetComponent<SpriteRenderer>());
	}
}
