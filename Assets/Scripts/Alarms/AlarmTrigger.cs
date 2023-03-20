using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public AudioClip alarmclip;

    private void Awake()
    {
        GetComponent<AudioSource>().pitch = 2.0f;
        GetComponent<AudioSource>().volume = 1;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            GetComponent<AudioSource>().Play();  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other = PlayerCollider)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}