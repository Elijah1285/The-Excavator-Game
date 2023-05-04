using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInTheChopper : MonoBehaviour
{
    bool flying = false;

    public GameObject player;
    public GameObject chopper;
    public Animation fly;
    public Vector3 vertical_velocity = new Vector3(0.0f, 0.5f, 0.0f);
    public Vector3 vertical_acceleration = new Vector3(0.0f, 0.5f, 0.0f);
    public Vector3 maximum_vertical_velocity = new Vector3(0.0f, 10.0f, 0.0f);
    private HashIDs hash;
    void Awake()
    {
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

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
        player.transform.parent = chopper.transform;
        Vector3 new_player_position = new Vector3(chopper.transform.position.x, chopper.transform.position.y + 1.25f, chopper.transform.position.z + 1);
        player.transform.position = new_player_position;
        player.GetComponent<PlayerMovement>().is_playing = false;
        player.GetComponent<Animator>().SetFloat(hash.speedFloat, 0);
        player.GetComponent<Animator>().SetBool(hash.backwardsBool, false);
        player.GetComponent<PlayerMovement>().noBackMov = true;
        player.GetComponent<AudioSource>().Stop();
        player.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(player.GetComponent<Rigidbody>());
        Quaternion player_rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        player.transform.rotation = player_rotation;
        flying = true;
    }

    private void Update()
    {
        if (flying)
        {
            chopper.transform.Translate(vertical_velocity * Time.deltaTime);

            if (vertical_velocity.y < maximum_vertical_velocity.y)
            {
                vertical_velocity += vertical_acceleration * Time.deltaTime;
            }
            else if (vertical_velocity.y > maximum_vertical_velocity.y)
            {
                vertical_velocity = maximum_vertical_velocity;
            }
        }
    }
}
