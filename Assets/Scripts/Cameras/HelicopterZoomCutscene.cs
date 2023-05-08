using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterZoomCutscene : MonoBehaviour
{
    public float dolly_zoom_timer = 3.0f;
    public bool dolly_zoom_timer_running = false;

    public Camera helicopter_dolly_zoom_camera;
    public Camera live_cam;
    public Camera prev_cam;

    public DollyZoom dolly_zoom;

    void Awake()
    {
        live_cam = Camera.allCameras[0];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            prev_cam = live_cam;
            live_cam.enabled = false;
            helicopter_dolly_zoom_camera.enabled = true;
            live_cam = Camera.allCameras[0];

            dolly_zoom.startDollyZoomEffect();
            dolly_zoom_timer_running = true;
        }
    }

    public void endCutscene()
    {
        helicopter_dolly_zoom_camera.enabled = false;
        prev_cam.enabled = true;
    }

    void Update()
    {
        if (dolly_zoom_timer_running)
        {
            dolly_zoom_timer -= Time.deltaTime;

            if (dolly_zoom_timer <= 0)
            {
                dolly_zoom.stopDollyZoomEffect();
                dolly_zoom_timer_running = false;
                dolly_zoom_timer = 3.0f;
            }
        }
    }
}
