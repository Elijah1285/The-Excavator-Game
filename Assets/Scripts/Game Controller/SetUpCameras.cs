using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{
    public Camera PlayerCamera;
    public Camera ExcavatorCamera;
    public Camera CabCamera;
    public Camera WheelCamera;
    public Camera HeliCamera;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        PlayerCamera.enabled = true;
        ExcavatorCamera.enabled = false;
        CabCamera.enabled = false;
        WheelCamera.enabled = false;
        HeliCamera.enabled = false;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        ExcavatorCamera.GetComponent<AudioListener>().enabled = false;
        CabCamera.GetComponent<AudioListener>().enabled = false;
        WheelCamera.GetComponent<AudioListener>().enabled = false;
        HeliCamera.GetComponent<AudioListener>().enabled = false;
    }
}
