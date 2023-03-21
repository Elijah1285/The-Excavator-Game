using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        MovementManagement(v);
    }

    void MovementManager(float vertical)
    {
        if (vertical > 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
            noBackMov = true;
        }
    }
}
