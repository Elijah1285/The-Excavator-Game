using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public bool in_push_range = false;
    public float x_movement = 0;

    public Vector3 pre_position;
    public Vector3 post_position;
    public Vector3 local_pre_position;
    public Vector3 local_post_position;
    public Vector3 velocity;
    public Vector3 local_velocity;
    public Animator anim;
    public Rigidbody rigidbody;

    public HashIDs hash;
    public EnterTheExcavator enter_the_excavator;

    void Start()
    {
        pre_position = transform.position;
        local_pre_position = transform.worldToLocalMatrix.MultiplyVector(pre_position);
    }

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
            velocity = rigidbody.velocity;
            local_velocity = transform.InverseTransformDirection(velocity);
            Debug.Log(local_velocity.z);



            //Vector3 local_forward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
            //Debug.DrawRay(transform.position, transform.forward);
            //post_position = transform.position;
            //local_post_position = transform.worldToLocalMatrix.MultiplyVector(post_position);

            //if (local_post_position.z < local_pre_position.z)
            //{    
            //    x_movement = local_post_position.z - local_pre_position.z;
            //    pre_position = post_position;
            //    local_pre_position = transform.worldToLocalMatrix.MultiplyVector(pre_position);
            //    //Debug.Log("moved forward");
            //    //Debug.Log(x_movement);
            //}
            //else if (local_post_position.z > local_pre_position.z)
            //{
            //    x_movement = local_post_position.z - local_pre_position.z;
            //    pre_position = post_position;
            //    local_pre_position = transform.worldToLocalMatrix.MultiplyVector(pre_position);
            //    //Debug.Log("moved backwards");
            //    //Debug.Log(x_movement);
            //}
            //else
            //{
            //    x_movement = 0;
            //}
        }
    }
}
