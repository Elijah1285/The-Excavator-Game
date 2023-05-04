using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInTheChopper : MonoBehaviour
{
    public GameObject player;
    public GameObject chopper;
    public Animation fly;

    private void OnTriggerStay(Collider other)
    {
        float enter = Input.GetAxis("Enter");

        if (enter > 0)
        {
            getIn();
        }
    }

    void getIn()
    {
        fly.Play();
    }
}
