using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public GameController GameController;
    public GameObject WinScreenUI;

    public GameObject Shadow;
    public float InteractionRange = 5.0f;

    void Update()
    {
        if (!Shadow.activeSelf && Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange)
            Shadow.SetActive(true);
        else if (Shadow.activeSelf)
            Shadow.SetActive(false);
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange)
            GameController.KeyPickup(WinScreenUI);
    }
	
}
