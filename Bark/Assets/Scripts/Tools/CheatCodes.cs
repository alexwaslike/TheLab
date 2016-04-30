using UnityEngine;

public class CheatCodes : MonoBehaviour {

    public GameController GameController;

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            TeleportToKey();
        } else if (Input.GetKeyDown(KeyCode.LeftControl)) {
            TeleportToBunker();
        }
    }

    private void TeleportToKey()
    {
        GameController.MainCharacterObj.transform.position = GameController.Key.transform.position;
    }

    private void TeleportToBunker()
    {
        GameController.MainCharacterObj.transform.position = new Vector3(GameController.EndBunker.transform.position.x + 2, GameController.EndBunker.transform.position.y - 2, 0.0f);
    }


}
