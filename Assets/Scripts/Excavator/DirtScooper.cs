using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScooper : MonoBehaviour
{
    public bool bucket_wheel_turning;

    private void OnTriggerStay(Collider other)
    {
        if (bucket_wheel_turning)
        {

        }
    }
}
