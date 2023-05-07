using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public Transform target;
    public Camera zoomCamera;

    private float init_height_at_distance = 0;
    private bool dolly_zoom_enabled = false;

    void Start()
    {
        startDollyZoomEffect();
    }

    void Update()
    {
        if (dolly_zoom_enabled)
        {
            var currDistance = Vector3.Distance(transform.position, target.position);
            zoomCamera.fieldOfView = FOVForHeightAndDistance(init_height_at_distance, currDistance);
        }

        if (Input.GetKey("[") || Input.GetKey("]"))
        {
            transform.Translate(Input.GetAxis("AltVertical") * Vector3.forward * Time.deltaTime);
        }
    }

    float frustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(zoomCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(init_height_at_distance * 0.5f / distance) * Mathf.Rad2Deg;
    }

    void startDollyZoomEffect()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        init_height_at_distance = frustumHeightAtDistance(distance);
        transform.LookAt(target.transform);
        dolly_zoom_enabled = true;
    }

    void stopDollyZoomEffect()
    {
        dolly_zoom_enabled = false;
    }
}
