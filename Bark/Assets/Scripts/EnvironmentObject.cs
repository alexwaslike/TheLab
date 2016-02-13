using UnityEngine;
using System.Collections;

public class EnvironmentObject : MonoBehaviour {

	public GameController GameController;

	void Start () {
		GameController.SetSortingOrder (gameObject);
	}
}
