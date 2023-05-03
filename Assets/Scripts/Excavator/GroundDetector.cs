using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool off_ground_timer_running = false;
    public float off_ground_timer = 3.0f;

    public bool excavator_reset_timer_running = false;
    public float excavator_reset_timer = 0.2f;

    public GameObject excavator;

    public Flash flash;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            off_ground_timer_running = false;
            off_ground_timer = 3.0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            off_ground_timer_running = true;
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
                off_ground_timer_running = false;
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
