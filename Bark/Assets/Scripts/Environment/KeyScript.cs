using UnityEngine;

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
        else
            Shadow.SetActive(false);
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, GameController.MainCharacter.transform.position) <= InteractionRange) {
            GameController.LevelGeneration.RemoveFromGrid(gameObject);
            GameController.MainCharacter.HasKey = true;
            Shadow.SetActive(false);
            GetComponent<SpriteRenderer>().sortingLayerName = "GameWorldUI";
            transform.parent = GameController.MainCharacter.transform;
            transform.localPosition = new Vector3(0.2f, 4.7f, 0.0f);
        }
            
    }


	
}
