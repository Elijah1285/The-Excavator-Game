using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{
    public Camera FollowCamera;
    public Camera StaticCamera;
    public Camera StaticCamera2;
    public Camera StaticCamera3;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        FollowCamera.enabled = true;
        StaticCamera.enabled = false;
        StaticCamera2.enabled = false;
        StaticCamera3.enabled = false;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        StaticCamera.GetComponent<AudioListener>().enabled = false;
        StaticCamera2.GetComponent<AudioListener>().enabled = false;
        StaticCamera3.GetComponent<AudioListener>().enabled = false;
    }
}
