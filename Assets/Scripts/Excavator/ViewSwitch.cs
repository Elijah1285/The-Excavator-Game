using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitch : MonoBehaviour
{
    public int cam_index;
    public bool cam_switch;
    public Camera cab_cam;
    public Camera wheel_cam;
    public Camera external_cam;
    public Camera current_cam;
    public ExcavatorMovement excavator_movement;

    private void Update()
    {
        if (excavator_movement.is_playing)
        {
            float cab_view = Input.GetAxis("CabView");
            float wheel_view = Input.GetAxis("WheelView");
            float external_view = Input.GetAxis("ExternalView");
            float view_switch = Input.GetAxis("ViewSwitch");

            if (cab_view > 0)
            {
                if (current_cam != cab_cam)
                {
                    Debug.Log("switched to cab view");
                    cam_index = 1;
                    cab_cam.enabled = true;
                    cab_cam.GetComponent<AudioListener>().enabled = true;
                    current_cam.enabled = false;
                    current_cam.GetComponent<AudioListener>().enabled = false;
                    current_cam = Camera.allCameras[0];
                }
            }
            else if (wheel_view > 0)
            {
                if (current_cam != wheel_cam)
                {
                    Debug.Log("switched to wheel view");
                    cam_index = 2;
                    wheel_cam.enabled = true;
                    wheel_cam.GetComponent<AudioListener>().enabled = true;
                    current_cam.enabled = false;
                    current_cam.GetComponent<AudioListener>().enabled = false;
                    current_cam = Camera.allCameras[0];
                }
            }
            else if (external_view > 0)
            {
                if (current_cam != external_cam)
                {
                    Debug.Log("switched to external view");
                    cam_index = 3;
                    external_cam.enabled = true;
                    external_cam.GetComponent<AudioListener>().enabled = true;
                    current_cam.enabled = false;
                    current_cam.GetComponent<AudioListener>().enabled = false;
                    current_cam = Camera.allCameras[0];
                }
            }
            
            if (view_switch > 0)
            {
                if (!cam_switch)
                {
                    cam_index++;

                    if (cam_index > 3)
                    {
                        cam_index = 1;
                    }

                    if (cam_index == 1)
                    {
                        cab_cam.enabled = true;
                        cab_cam.GetComponent<AudioListener>().enabled = true;
                    }
                    if (cam_index == 2)
                    {
                        wheel_cam.enabled = true;
                        wheel_cam.GetComponent<AudioListener>().enabled = true;
                    }
                    if (cam_index == 3)
                    {
                        external_cam.enabled = true;
                        external_cam.GetComponent<AudioListener>().enabled = true;
                    }

                    current_cam.enabled = false;
                    current_cam.GetComponent<AudioListener>().enabled = false;
                    current_cam = Camera.allCameras[0];

                    cam_switch = true;
                }
            }
            else
            {
                if (cam_switch)
                {
                    cam_switch = false;
                }
            }
        }
    }
}
