using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelicopterZoomCutscene : MonoBehaviour
{
    public float dolly_zoom_timer = 1.0f;
    public bool dolly_zoom_timer_running = false;
    public bool cutscene_played = false;

    public TMP_Text minimap_toggle_instruction;

    public Camera helicopter_dolly_zoom_camera;
    public Camera live_cam;
    public Camera prev_cam;

    public DollyZoom dolly_zoom;
    public MusicPlayer music_player;

    void Awake()
    {
        live_cam = Camera.allCameras[0];
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Excavator") && !cutscene_played)
        {
            prev_cam = live_cam;
            live_cam.enabled = false;
            helicopter_dolly_zoom_camera.enabled = true;
            live_cam = Camera.allCameras[0];

            dolly_zoom.startDollyZoomEffect();
            dolly_zoom_timer_running = true;

            if (minimap_toggle_instruction.enabled)
            {
                minimap_toggle_instruction.enabled = false;
            }

            music_player.playZoomSound();

            cutscene_played = true;
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
                dolly_zoom_timer = 1.0f;
            }
        }
    }
}
