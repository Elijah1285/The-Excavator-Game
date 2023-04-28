using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitch : MonoBehaviour
{
    public int cam_index;
    public Camera cab_cam;
    public Camera wheel_cam;
    public Camera external_cam;
    public Camera live_cam;
    public ExcavatorMovement excavator_movement;

    private void Awake()
    {
        live_cam = Camera.allCameras[0];
    }

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
                if (live_cam != cab_cam)
                {
                    cam_index = 1;
                    cab_cam.enabled = true;
                    cab_cam.GetComponent<AudioListener>().enabled = true;
                    live_cam = Camera.allCameras[0];
                }
            }
            else if (wheel_view > 0)
            {
                if (live_cam != wheel_cam)
                {
                    cam_index = 2;
                    wheel_cam.enabled = true;
                    wheel_cam.GetComponent<AudioListener>().enabled = true;
                    live_cam = Camera.allCameras[0];
                }
            }
            else if (external_view > 0)
            {
                if (live_cam != external_cam)
                {
                    cam_index = 3;
                    external_cam.enabled = true;
                    external_cam.GetComponent<AudioListener>().enabled = true;
                    live_cam = Camera.allCameras[0];
                }
            }
            else if (view_switch > 0)
            {
                cam_index++;

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

                live_cam.enabled = false;
                live_cam.GetComponent<AudioListener>().enabled = false;
                live_cam = Camera.allCameras[0];

                if (cam_index > 3)
                {
                    cam_index = 1;
                }
            }
        }
    }
}
