using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kill : MonoBehaviour
{
    public enum ProjectileState {LOADED, FIRING};
    public ProjectileState projectile_state = ProjectileState.LOADED;

    public bool destroying = false;

    public Transform cam_parent;
    public Camera player_cam;
    public Camera projectile_cam;
    public TMP_Text dirt_destroyed;

    public GameObject this_object;

    public MusicPlayer music_player;
    public DirtDestroyedCounter dirt_destroyed_counter;

    void Awake()
    {
        this_object = gameObject;
        cam_parent = GameObject.Find("Cameras").transform;
        player_cam = GameObject.Find("Player Camera").GetComponent<Camera>() as Camera;
        projectile_cam = GameObject.Find("projectile_cam").GetComponent<Camera>() as Camera;
        dirt_destroyed = GameObject.Find("dirt_destroyed").GetComponent<TMP_Text>();
        music_player = GameObject.FindObjectOfType<MusicPlayer>();
        dirt_destroyed_counter = GameObject.FindObjectOfType<DirtDestroyedCounter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (projectile_state == ProjectileState.FIRING && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Catapult")
        {
            if (collision.gameObject.tag == "DirtBall" || collision.gameObject.tag == "DirtBall2" || collision.gameObject.tag == "DirtBall3")
            {
                Destroy(collision.gameObject);
                music_player.playDirtDestroyedSound();

                if (!dirt_destroyed.enabled)
                {
                    dirt_destroyed.enabled = true;
                    dirt_destroyed.GetComponent<DirtDestroyedTextTimer>().timer_running = true;
                }
                else
                {
                    dirt_destroyed.GetComponent<DirtDestroyedTextTimer>().timer = 3.0f;
                }

                dirt_destroyed_counter.incrementDirtDestroyedNum();
            }

            if (projectile_cam.enabled)
            {
                projectile_cam.enabled = false;
            }

            if (!player_cam.enabled)
            {
                player_cam.enabled = true;
            }

            if (projectile_cam.transform.parent != cam_parent)
            {
                projectile_cam.transform.parent = cam_parent;
            }

            if (!destroying)
            {
                Destroy(gameObject, 3);
                destroying = true;
            }
        }
    }

    public void prepareForLaunch()
    {
        projectile_state = ProjectileState.FIRING;
        this_object.AddComponent(typeof(Rigidbody));
        this_object.GetComponent<Rigidbody>().freezeRotation = true;
    }
}
