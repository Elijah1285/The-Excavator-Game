using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NoFogOnCamera : MonoBehaviour
{
    public bool allow_fog = false;

    private bool fog_on;

    void OnPreRender()
    {
        fog_on = RenderSettings.fog;
        RenderSettings.fog = allow_fog;
    }

    void OnPostRender()
    {
        RenderSettings.fog = fog_on;
    }
}
