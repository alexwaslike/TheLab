using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void PlayClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void CreditsClick()
    {
        SceneManager.LoadScene("Credits");
    } 

    public void ExitClick()
    {
        Application.Quit();
    }
}
