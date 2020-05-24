using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class myTimer : MonoBehaviour {

    public float myCoolTimer;

    public Text timerText;

    private PauseMenu thePauseMenu;

    public GameObject gameOverScreen;

    private BarScript bS;
    

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Timer"))
        {
            if (Application.loadedLevel == 1)
            {
                myCoolTimer = 120;
            }
            else
            {
                myCoolTimer = PlayerPrefs.GetFloat("Timer");
            }

        }
        else
        {
            Debug.Log("not timer key");
        }




        timerText = GetComponent<Text>();

        thePauseMenu = FindObjectOfType<PauseMenu>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (thePauseMenu.isPaused)
            return;
        
        myCoolTimer -= Time.deltaTime;

        if (myCoolTimer <= 0)
        {
            
            gameOverScreen.SetActive(true);

        }
        timerText.text = "Timer: " + Mathf.Round(myCoolTimer);

        //timerText.text = myCoolTimer.ToString("f0");
        //print(myCoolTimer);
	}

    

  
}
