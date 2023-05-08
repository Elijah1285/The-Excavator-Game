using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public Camera this_camera;
    private float mouseX;
    private float mouseY;
    private float mouseZ;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this_camera.enabled)
        {
            RaycastHit hit;
            Ray ray = this_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);

                if (hit.collider.gameObject.tag != "Ground")
                {
                    if (hit.collider.gameObject.tag != "Player")
                    {
                        Debug.Log("occlusion");
                    }
                }
            }

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            mouseZ = Input.GetAxis("Mouse ScrollWheel");


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

            float dist = Vector3.Distance(target.transform.position, transform.position);

            if (mouseZ < 0 && dist < 50)
            {
                offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
            }


            if (mouseZ > 0 && dist > 0.5)
            {
                offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
            }

            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position + (rotation * offset);
            transform.LookAt(target.transform);
        }
    }
}

