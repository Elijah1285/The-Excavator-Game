using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kill : MonoBehaviour
{
    public bool destroying = false;

    public Transform cam_parent;
    public Camera player_cam;
    public Camera projectile_cam;
    public TMP_Text dirt_destroyed;

    void Awake()
    {
        cam_parent = GameObject.Find("Cameras").transform;
        player_cam = GameObject.Find("Player Camera").GetComponent<Camera>() as Camera;
        projectile_cam = GameObject.Find("projectile_cam").GetComponent<Camera>() as Camera;
        dirt_destroyed = GameObject.Find("dirt_destroyed").GetComponent<TMP_Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DirtBall" || collision.gameObject.tag == "DirtBall2" || collision.gameObject.tag == "DirtBall3")
        {
            Destroy(collision.gameObject);

            if (!dirt_destroyed.enabled)
            {
                dirt_destroyed.enabled = true;
                dirt_destroyed.GetComponent<DirtDestroyedTextTimer>().timer_running = true;
            }
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
