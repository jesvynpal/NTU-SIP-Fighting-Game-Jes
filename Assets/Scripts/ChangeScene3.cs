using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene3 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("title_menu");
        }
    }
}
