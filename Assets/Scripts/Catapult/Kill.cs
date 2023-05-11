using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public Transform cam_parent;
    public Camera player_cam;
    public Camera projectile_cam;

    void Awake()
    {
        cam_parent = GameObject.Find("Cameras").transform;
        player_cam = GameObject.Find("Player Camera").GetComponent<Camera>() as Camera;
        projectile_cam = GameObject.Find("projectile_cam").GetComponent<Camera>() as Camera;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DirtBall" || collision.gameObject.tag == "DirtBall2" || collision.gameObject.tag == "DirtBall3")
        {
            Destroy(collision.gameObject);
        }

        projectile_cam.enabled = false;
        player_cam.enabled = true;

        projectile_cam.transform.parent = cam_parent;

        Destroy(gameObject, 3);
    }
}
