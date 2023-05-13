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

    public Animator player_anim;
    public Animator excavator_anim;

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
    public HashIDs hash;

    void Awake()
    {
        live_cam = Camera.allCameras[0];
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player" || (other.gameObject.tag == "Excavator" && enter_the_excavator.player_in_excavator)) && !cutscene_played)
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
            excavator_movement.cutscene_playing = true;
            enter_the_excavator.cutscene_playing = true;

            player_anim.SetFloat(hash.speedFloat, 0.0f);
            player_anim.SetBool(hash.sneakingBool, false);
            player_anim.SetBool(hash.backwardsBool, false);

            excavator_anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);
            excavator_anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);
            excavator_anim.SetFloat(hash.armSpeedFloat, 0.0f);
            player_movement.stopWalkAudio();
            excavator_movement.stopMovementAudio();
            excavator_movement.stopArmAudio();

            cutscene_played = true;
        }
    }

    public void endCutscene()
    {
        dolly_zoom_camera.enabled = false;
        prev_cam.enabled = true;

        set_up_cameras.cutscene_playing = false;
        player_movement.can_move = true;
        excavator_movement.cutscene_playing = false;
        enter_the_excavator.cutscene_playing = false;

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
