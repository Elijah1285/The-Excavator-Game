using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public float zooming_speed = 15.0f;
    public float cutscene_end_timer = 3.0f;

    public Transform target;
    public Camera zoomCamera;

    private float init_height_at_distance = 0;
    private bool dolly_zoom_enabled = false;
    public bool slowing_down = false;
    public bool cutscene_end_timer_running = false;

    public HelicopterZoomCutscene helicopter_zoom_cutscene;

    void Update()
    {
        if (dolly_zoom_enabled)
        {
            var currDistance = Vector3.Distance(transform.position, target.position);
            zoomCamera.fieldOfView = FOVForHeightAndDistance(init_height_at_distance, currDistance);
            transform.Translate(Vector3.forward * Time.deltaTime * zooming_speed);

            if (slowing_down)
            {
                zooming_speed -= 20 * Time.deltaTime;

                if (zooming_speed <= 0)
                {
                    dolly_zoom_enabled = false;
                    zooming_speed = 15.0f;
                    cutscene_end_timer_running = true;
                }
            }
        }

        if (cutscene_end_timer_running)
        {
            cutscene_end_timer -= Time.deltaTime;

            if (cutscene_end_timer <= 0)
            {
                helicopter_zoom_cutscene.endCutscene();
                cutscene_end_timer_running = false;
                cutscene_end_timer = 3.0f;
            }
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

    public void startDollyZoomEffect()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        init_height_at_distance = frustumHeightAtDistance(distance);
        transform.LookAt(target.transform);
        dolly_zoom_enabled = true;
    }

    public void stopDollyZoomEffect()
    {
        slowing_down = true;
    }
}
