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

    public Flash flash;
    public EnterTheExcavator enter_the_excavator;

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
            }
        }
    }

    void Update()
    {
        Debug.Log(ground_intersected);

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
