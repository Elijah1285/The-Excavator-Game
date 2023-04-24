using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        float enter = Input.GetAxis("Enter");
        float exit = Input.GetAxis("Exit");

        if (enter > 0)
        {
            SceneManager.LoadScene("Gameplay");
        }

        if (exit > 0)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
            //
        }
    }
}
