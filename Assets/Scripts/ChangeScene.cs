using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

    private GameManager gm; //
    public myTimer mt;
    public BarScript hp;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SaveScore();
            SaveTimer();
            SaveHealth();
            SceneManager.LoadScene("Jess2");
        }
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("Coin", gm.collectedCoins); //
    }

    void SaveTimer()
    {
        PlayerPrefs.SetFloat("Timer", mt.myCoolTimer);
    }

    void SaveHealth()
    {
        PlayerPrefs.SetFloat("Health", hp.Value);
    }
}
