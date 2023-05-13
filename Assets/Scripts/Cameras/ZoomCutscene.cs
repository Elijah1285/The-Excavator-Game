using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoomCutscene : MonoBehaviour
{
    public float dolly_zoom_timer = 0;
    public bool dolly_zoom_timer_running = false;
    public bool cutscene_played = false;

    public TMP_Text minimap_toggle_instruction;

    public Camera dolly_zoom_camera;
    public Camera live_cam;
    public Camera prev_cam;
    public Camera minimap;

    public DollyZoom dolly_zoom;
    public MusicPlayer music_player;
    public SetUpCameras set_up_cameras;
    public PlayerMovement player_movement;
    public ExcavatorMovement excavator_movement;
    public EnterTheExcavator enter_the_excavator;

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
            dolly_zoom_camera.enabled = true;
            live_cam = Camera.allCameras[0];

            dolly_zoom.startDollyZoomEffect();
            dolly_zoom_timer_running = true;

            if (minimap_toggle_instruction.enabled)
            {
                minimap_toggle_instruction.enabled = false;
            }

            music_player.playZoomSound();

            set_up_cameras.cutscene_playing = true;
            player_movement.can_move = false;
            excavator_movement.can_move = false;
            enter_the_excavator.excavator_open = false;

            cutscene_played = true;
        }
    }

    public void endCutscene()
    {
        dolly_zoom_camera.enabled = false;
        prev_cam.enabled = true;

        set_up_cameras.cutscene_playing = false;
        player_movement.can_move = true;
        excavator_movement.can_move = true;
        enter_the_excavator.excavator_open = true;

        minimap.depth = 0;
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
