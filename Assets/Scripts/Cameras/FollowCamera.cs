using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public enum Target {PLAYER, EXCAVATOR};

    public Target target_name = Target.PLAYER;

    public Vector3 desired_cam_position;
    public Vector3 cam_direction;
    public Vector3 smooth_follow;
    public float cam_distance = 0;
    public float cam_min_distance = 0;
    public float cam_max_distance = 0;
    public float smooth_speed = 0.1f;
    public float angle_between = 0;
    public float desired_angle = 0;

    public GameObject target;
    public Vector3 offset;
    public Camera this_camera;
    private float mouseX;
    private float mouseY;
    private float mouseZ;

    public Transform cam_transform;
    public Transform behind_cam_position_transform;

    public Quaternion rotation;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if (this_camera.enabled)
        {
            cam_direction = cam_transform.TransformDirection(Vector3.forward);
            cam_distance = Vector3.Distance(target.transform.position, transform.position);

            checkOccllusion();

            angle_between = Vector3.Angle(Vector3.up, transform.forward);

            desired_angle = target.transform.eulerAngles.y;

            smoothFollow();
        }
    }

    void checkOccllusion()
    {
        RaycastHit cam_ray_hit;
        RaycastHit behind_position_ray_hit;
        Ray ray_from_cam = this_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray ray_from_behind_position = new Ray(behind_cam_position_transform.position, cam_direction);

        if (Physics.Raycast(ray_from_cam, out cam_ray_hit))
        {
            if (((target_name == Target.PLAYER && cam_ray_hit.collider.gameObject.tag != "Player") || (target_name == Target.EXCAVATOR && cam_ray_hit.collider.gameObject.tag != "Excavator")) && cam_distance > cam_min_distance)
            {
                offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
            }
            else
            {
                if (cam_distance < cam_max_distance)
                {
                    Physics.Raycast(ray_from_behind_position, out behind_position_ray_hit);

                    if ((target_name == Target.PLAYER && behind_position_ray_hit.collider.gameObject.tag == "Player") || (target_name == Target.EXCAVATOR && behind_position_ray_hit.collider.gameObject.tag == "Excavator"))
                    {
                        offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
                    }
                }
            }
        }
    }

    void smoothFollow()
    {
        rotation = Quaternion.Euler(0, desired_angle, 0);
        smooth_follow = Vector3.Lerp(cam_transform.position, target.transform.position + (rotation * offset), smooth_speed);

        cam_transform.position = smooth_follow;
        transform.LookAt(target.transform);
    }
}

