using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

	public void Resume()
    {
        gameObject.SetActive(false);
    }  

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
