using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public string startLevel;

	public void RestartGame()
    {
        SceneManager.LoadScene("title_menu");
    }
}
