using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int menu = 0;

    private void Update()
    {
        float enter = Input.GetAxis("Enter");
        float exit = Input.GetAxis("Exit");

        if (enter > 0)
        {
            if (menu == 1)
            {
                SceneManager.LoadScene("Instructions");
            }
            else if (menu == 2)
            {
                SceneManager.LoadScene("Gameplay");
            }
        }

        if (exit > 0)
        {
            if (menu == 1)
            {
                Application.Quit();
            }
            else if (menu == 2)
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }
    }
}
