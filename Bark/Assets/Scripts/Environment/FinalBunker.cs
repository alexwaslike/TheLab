using UnityEngine;

public class FinalBunker : MonoBehaviour
{
    public GameController GameController;
    public GameObject BunkerUI;

    public void OnMouseUp()
    {
        if (GameController.MainCharacter.HasKey)
            GameController.WinGame();
        else
            BunkerUI.SetActive(true);
    }
}
