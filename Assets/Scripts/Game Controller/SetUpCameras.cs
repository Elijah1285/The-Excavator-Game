using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetUpCameras : MonoBehaviour
{
    public bool toggled_minimap = false;

    public Camera PlayerCamera;
    public Camera ExcavatorCamera;
    public Camera CabCamera;
    public Camera WheelCamera;
    public Camera HeliCamera;
    public Camera minimap_cam;

    public TMP_Text minimap_toggle_instruction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        PlayerCamera.enabled = true;
        ExcavatorCamera.enabled = false;
        CabCamera.enabled = false;
        WheelCamera.enabled = false;
        HeliCamera.enabled = false;
        minimap_cam.enabled = true;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        ExcavatorCamera.GetComponent<AudioListener>().enabled = false;
        CabCamera.GetComponent<AudioListener>().enabled = false;
        WheelCamera.GetComponent<AudioListener>().enabled = false;
        HeliCamera.GetComponent<AudioListener>().enabled = false;
    }

    void Update()
    {
        float map_toggle = Input.GetAxis("MapToggle");

        if (map_toggle > 0 && !toggled_minimap)
        {
            minimap_cam.enabled = !minimap_cam.enabled;

            if (minimap_toggle_instruction.enabled)
            {
                minimap_toggle_instruction.enabled = false;
            }

            toggled_minimap = true;
        }
        else if (map_toggle <= 0)
        {
            if (toggled_minimap)
            {
                toggled_minimap = false;
            }
        }
    }
}
