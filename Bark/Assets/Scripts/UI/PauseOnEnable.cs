using UnityEngine;
using System.Collections;

public class PauseOnEnable : MonoBehaviour {

	public GameController GameController;

	void OnEnable(){
		GameController.PauseGame (true);
	}

	void OnDisable(){
		GameController.PauseGame (false);
	}


}
