using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool off_ground_timer_running = false;
    public float off_ground_timer = 3.0f;

    public GameObject excavator;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            off_ground_timer_running = false;
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
                resetExcavator();
                off_ground_timer = 3.0f;
                off_ground_timer_running = false;
            }
        }
    }

    void resetExcavator()
    {
        Quaternion upright = Quaternion.Euler(0, 0, 0);

        excavator.transform.rotation = upright;
    }
}
