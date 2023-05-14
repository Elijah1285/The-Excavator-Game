using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckifQuit : MonoBehaviour
{
    void Update()
    {
        bool exit = Input.GetButtonDown("Exit");

        if (exit)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
