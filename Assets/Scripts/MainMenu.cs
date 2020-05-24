using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //public string startLevel;

    public void NewGame()
    {
       // Application.LoadLevel(startLevel);
        SceneManager.LoadScene("Jess");
    }

    public void QuitGame()
    {
        Application.Quit ();
    }
}
