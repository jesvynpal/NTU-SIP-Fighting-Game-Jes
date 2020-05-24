using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour {

    private GameManager gm;
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
            SceneManager.LoadScene("Jess3");
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
