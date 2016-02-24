using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public GameController controller;
    public GameObject WinScreenUI;
    void OnMouseUp()
    {
        this.gameObject.SetActive(false);
        controller.KeyPickup(WinScreenUI);
    }
	
}
