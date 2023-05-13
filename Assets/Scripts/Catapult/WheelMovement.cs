using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public bool in_push_range = false;
    public float x_movement = 0;
    public float speed = 0;

    public Vector3 velocity;
    public Vector3 local_velocity;
    public Animator anim;

    public HashIDs hash;
    public EnterTheExcavator enter_the_excavator;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Excavator" && enter_the_excavator.player_in_excavator))
        {
            in_push_range = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Excavator" && enter_the_excavator.player_in_excavator))
        {
            in_push_range = false;
        }
    }

    void Update()
    {
        if (in_push_range)
        {
            velocity = GetComponent<Rigidbody>().velocity;
            local_velocity = transform.InverseTransformDirection(velocity);
            speed = -local_velocity.z;
            anim.SetFloat(hash.wheelSpeedFloat, speed);
        }
    }
}
