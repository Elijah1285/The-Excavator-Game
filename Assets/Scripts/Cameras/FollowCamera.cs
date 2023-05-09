using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public enum Target {PLAYER, EXCAVATOR};

    public Target target_name = Target.PLAYER;

    public Vector3 cam_direction;
    public float cam_distance = 0;
    public float cam_min_distance = 0;
    public float cam_max_distance = 0;

    public GameObject target;
    public Vector3 offset;
    public Camera this_camera;
    private float mouseX;
    private float mouseY;
    private float mouseZ;

    public Transform cam_transform;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
        cam_direction = cam_transform.localPosition.normalized;
        cam_distance = cam_max_distance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this_camera.enabled)
        {
            cam_direction = cam_transform.localPosition.normalized;
            cam_direction = cam_transform.TransformDirection(Vector3.forward);
            cam_distance = Vector3.Distance(target.transform.position, transform.position);

            Transform camera_transform = this_camera.transform;

            RaycastHit hit;
            Ray ray_forward = this_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Ray ray_backward = new Ray(camera_transform.position, -camera_transform.forward);
            //Debug.DrawRay(camera_transform.position, Vector3.Scale(-camera_transform.forward, new Vector3(100.0f, 100.0f, 100.0f)));
            Debug.DrawRay(camera_transform.position, Vector3.Scale(cam_direction, new Vector3(100.0f, 100.0f, 100.0f)));

            if (Physics.Raycast(ray_forward, out hit))
            {
                if (((target_name == Target.PLAYER && hit.collider.gameObject.tag != "Player") || (target_name == Target.EXCAVATOR && hit.collider.gameObject.tag != "Excavator")) && cam_distance > cam_min_distance)
                {
                    offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
                }
                else
                {
                    if (!Physics.Raycast(ray_backward, 80.0f) && cam_distance < cam_max_distance)
                    {
                        Debug.Log("zooming out");
                        offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
                    }
                }
            }

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            if (Input.GetMouseButton(1))
            {
                offset = Quaternion.Euler(0, mouseX, 0) * offset;
            }

            float angleBetween = Vector3.Angle(Vector3.up, transform.forward);

            float desiredAngle = target.transform.eulerAngles.y;

            if (((angleBetween > 100) && (mouseY < 0)) || ((angleBetween < 145) && (mouseY > 0)))
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
                    offset = Quaternion.AngleAxis(mouseY, LocalRight) * offset;
                }
            }

            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position + (rotation * offset);
            transform.LookAt(target.transform);
            //checkOcclusion(camera_transform);
        }
    }
}

