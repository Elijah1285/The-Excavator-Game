using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetInTheChopper : MonoBehaviour
{
    public float win_timer = 15.0f;
    public bool flying = false;

    public GameObject player;
    public GameObject chopper;
    public Animation fly;
    public Camera heli_cam;
    public Camera live_cam;
    public AudioListener player_audio;
    public AudioSource heli_audio;
    public Vector3 vertical_velocity = new Vector3(0.0f, 0.5f, 0.0f);
    public Vector3 vertical_acceleration = new Vector3(0.0f, 0.5f, 0.0f);
    public Vector3 maximum_vertical_velocity = new Vector3(0.0f, 10.0f, 0.0f);
    private HashIDs hash;
    void Awake()
    {
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        live_cam = Camera.allCameras[0];
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

        heli_audio.Play();

        live_cam.enabled = false;
        player_audio.enabled = false;
        heli_cam.enabled = true;
        heli_cam.GetComponent<AudioListener>().enabled = true;
        live_cam = Camera.allCameras[0];
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

            win_timer -= Time.deltaTime;

            if (win_timer <= 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
