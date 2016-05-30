using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject menu;

    private bool gameActive;

    void Start()
    {
        menu.SetActive(true);
        gameActive = false;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && gameActive == false)
        {
            gameActive = true;
            Time.timeScale = 1f;
            menu.SetActive(false);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel("Test");
        }
    }
}
