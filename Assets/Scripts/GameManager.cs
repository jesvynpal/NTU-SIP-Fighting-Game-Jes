using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

   

    [SerializeField]
    private GameObject coinPrefab;

    
   

    [SerializeField]
    private Text coinTxt;

   
    public int collectedCoins; 
    

    public static GameManager Instance
    {
        get
        {
           if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }

    }

    public GameObject CoinPrefab
    {
        get
        {
            return coinPrefab;
        }
        
    }

   

    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }

        set
        {
            coinTxt.text = value.ToString();
            collectedCoins = value;
        }
    }

  
    void Start()
    {
        if(PlayerPrefs.HasKey("Coin"))
        {
            if (Application.loadedLevel == 1)
            {
                PlayerPrefs.DeleteKey("Coin");
                collectedCoins = 0;
            }
            else
            {
                collectedCoins = PlayerPrefs.GetInt("Coin");
            }
            
        }

    }

    void Update()
    {
        coinTxt.text = ("" + collectedCoins);
      

    }
}
