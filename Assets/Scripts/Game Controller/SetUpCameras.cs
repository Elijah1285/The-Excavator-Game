using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{
    public Camera FollowCamera;
    public Camera StaticCamera;
    public Camera StaticCamera2;
    public Camera StaticCamera3;
    public Camera FollowCamera2;
    public Camera CabCamera;
    public Camera WheelCamera;
    public Camera HeliCamera;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        FollowCamera.enabled = true;
        StaticCamera.enabled = false;
        StaticCamera2.enabled = false;
        StaticCamera3.enabled = false;
        FollowCamera2.enabled = false;
        CabCamera.enabled = false;
        WheelCamera.enabled = false;
        HeliCamera.enabled = false;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        StaticCamera.GetComponent<AudioListener>().enabled = false;
        StaticCamera2.GetComponent<AudioListener>().enabled = false;
        StaticCamera3.GetComponent<AudioListener>().enabled = false;
        FollowCamera2.GetComponent<AudioListener>().enabled = false;
        CabCamera.GetComponent<AudioListener>().enabled = false;
        WheelCamera.GetComponent<AudioListener>().enabled = false;
        HeliCamera.GetComponent<AudioListener>().enabled = false;
    }
}
