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
        if (Input.GetKeyUp("1"))
        {
            if (!player_in_excavator && player_in_bound)
            {
                player_in_excavator = true;
                player_cam.enabled = false;
                excavator_cam.enabled = true;
                player.GetComponent<PlayerMovement>().is_playing = false;
                player.transform.position = new Vector3(excavator.transform.position.x, excavator.transform.position.y + 8, excavator.transform.position.z);
                player.transform.parent = excavator.transform;
                excavator.GetComponent<ExcavatorMovement>().is_playing = true;
                excavator.GetComponent<AudioSource>().Play();
                Input.ResetInputAxes();
            }
            else if (player_in_excavator)
            {
                player_in_excavator = false;
                player_cam.enabled = true;
                excavator_cam.enabled = false;
                player.GetComponent<PlayerMovement>().is_playing = true;
                player.transform.position = new Vector3(excavator.transform.position.x + 4, excavator.transform.position.y, excavator.transform.position.z - 6);
                player.transform.parent = null;
                excavator.GetComponent<ExcavatorMovement>().is_playing = false;
                excavator.GetComponent<AudioSource>().Stop();
                Input.ResetInputAxes();
            }
        }
    }
}
