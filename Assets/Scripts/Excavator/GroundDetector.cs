using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool off_ground_timer_running = false;
    public float off_ground_timer = 3.0f;

    public bool excavator_reset_timer_running = false;
    public float excavator_reset_timer = 0.2f;

    public int ground_intersected = 0;

    public GameObject excavator;
    public Animator anim;
    public AudioSource drive_audio;
    public AudioSource turn_audio;

    public Flash flash;
    public EnterTheExcavator enter_the_excavator;
    public ExcavatorMovement excavator_movement;
    public HashIDs hash;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            ground_intersected++;

            if (off_ground_timer_running)
            {
                off_ground_timer_running = false;
                off_ground_timer = 3.0f;
            }

            if (!enter_the_excavator.excavator_open)
            {
                enter_the_excavator.excavator_open = true;
                
            }

            if (!excavator_movement.can_move)
            {
                excavator_movement.can_move = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            ground_intersected--;

            if (ground_intersected <= 0)
            {
                off_ground_timer_running = true;
                enter_the_excavator.excavator_open = false;
                excavator_movement.can_move = false;
                anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);               
                anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);
                excavator_movement.stopMovementAudio();
                excavator_movement.setMovementToAir();
            }
        }
    }

    void Update()
    {
        if (off_ground_timer_running)
        {
            off_ground_timer -= Time.deltaTime;

            if (off_ground_timer <= 0)
            {
                flash.startFlash(0.4f, 1.0f, Color.black);
                off_ground_timer = 3.0f;
                excavator_reset_timer_running = true;
            }
        }

        if (excavator_reset_timer_running)
        {
            excavator_reset_timer -= Time.deltaTime;

            if (excavator_reset_timer <= 0)
            {
                resetExcavator();

                excavator_reset_timer = 0.2f;
                excavator_reset_timer_running = false;
            }
        }
    }

    void resetExcavator()
    {
        Quaternion upright = Quaternion.Euler(0, 0, 0);

        excavator.transform.rotation = upright;
    }
}
