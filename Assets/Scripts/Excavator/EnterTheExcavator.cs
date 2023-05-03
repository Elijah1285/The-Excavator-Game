using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheExcavator : MonoBehaviour
{
    public bool player_in_bound = false;
    public bool player_in_excavator = false;
    public Camera player_cam;
    public Camera excavator_cam;
    public GameObject player;
    public GameObject excavator;
    public ViewSwitch view_switch;
    public AudioListener player_audio_listener;
    private HashIDs hash;

    public ParticleSystem pipe_1_particles;
    public ParticleSystem pipe_2_particles;
    public ParticleSystem pipe_3_particles;

    void Awake()
    {
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            player_in_bound = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            player_in_bound = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp("e"))
        {
            if (!player_in_excavator && player_in_bound)
            {
                player_in_excavator = true;
                player_cam.enabled = false;
                player_audio_listener.enabled = false;
                view_switch.current_cam.enabled = true;
                view_switch.current_cam.GetComponent<AudioListener>().enabled = true;
                player.GetComponent<PlayerMovement>().is_playing = false;
                player.transform.position = new Vector3(excavator.transform.position.x + 4, excavator.transform.position.y + 5, excavator.transform.position.z);
                player.transform.parent = excavator.transform;
                player.GetComponent<Animator>().SetFloat(hash.speedFloat, 0);
                player.GetComponent<Animator>().SetBool(hash.backwardsBool, false);
                player.GetComponent<PlayerMovement>().noBackMov = true;
                player.GetComponent<AudioSource>().Stop();
                player.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(player.GetComponent<Rigidbody>());
                Quaternion player_rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                player.transform.rotation = player_rotation;
                excavator.GetComponent<ExcavatorMovement>().is_playing = true;
                excavator.GetComponent<ExcavatorMovement>().engine_start = true;
                excavator.GetComponent<ExcavatorMovement>().engine_audio_source.Play();
                Input.ResetInputAxes();
            }
            else if (player_in_excavator)
            {
                player_in_excavator = false;
                player_cam.enabled = true;
                player_audio_listener.enabled = true;
                view_switch.current_cam.enabled = false;
                view_switch.current_cam.GetComponent<AudioListener>().enabled = false;
                player.GetComponent<PlayerMovement>().is_playing = true;
                player.transform.position = new Vector3(excavator.transform.position.x + 4, excavator.transform.position.y, excavator.transform.position.z - 6);
                player.transform.parent = null;
                player.GetComponent<Animator>().SetFloat(hash.speedFloat, 0);
                player.GetComponent<Animator>().SetBool(hash.backwardsBool, false);
                player.GetComponent<PlayerMovement>().noBackMov = true;
                player.GetComponent<CapsuleCollider>().enabled = true;
                player.AddComponent(typeof(Rigidbody));
                player.GetComponent<Rigidbody>().freezeRotation = true;
                Quaternion upright = Quaternion.Euler(0, 0, 0);
                player.transform.rotation = upright;
                excavator.GetComponent<ExcavatorMovement>().is_playing = false;
                excavator.GetComponent<Animator>().SetFloat(hash.leftTrackSpeedFloat, 0);
                excavator.GetComponent<Animator>().SetFloat(hash.rightTrackSpeedFloat, 0);
                excavator.GetComponent<Animator>().SetFloat(hash.armSpeedFloat, 0);
                excavator.GetComponent<Animator>().SetFloat(hash.bucketWheelSpeedFloat, 0);
                excavator.GetComponent<ExcavatorMovement>().engine_audio_source.Stop();
                excavator.GetComponent<ExcavatorMovement>().bucket_wheel_speed = 0;

                if (excavator.GetComponent<ExcavatorMovement>().bucket_wheel_audio_source.isPlaying)
                {
                    excavator.GetComponent<ExcavatorMovement>().bucket_wheel_audio_source.Stop();
                }

                if (excavator.GetComponent<ExcavatorMovement>().arm_audio_source.isPlaying)
                {
                    excavator.GetComponent<ExcavatorMovement>().arm_audio_source.Stop();
                }

                if (excavator.GetComponent<ExcavatorMovement>().turn_audio_source.isPlaying)
                {
                    excavator.GetComponent<ExcavatorMovement>().turn_audio_source.Stop();
                }

                if (excavator.GetComponent<ExcavatorMovement>().drive_audio_source.isPlaying)
                {
                    excavator.GetComponent<ExcavatorMovement>().drive_audio_source.Stop();
                }

                if (pipe_1_particles.isEmitting)
                {
                    pipe_1_particles.Stop();
                }
                if (pipe_2_particles.isEmitting)
                {
                    pipe_2_particles.Stop();
                }
                if (pipe_3_particles.isEmitting)
                {
                    pipe_3_particles.Stop();
                }

                Input.ResetInputAxes();
            }
        }
    }
}
