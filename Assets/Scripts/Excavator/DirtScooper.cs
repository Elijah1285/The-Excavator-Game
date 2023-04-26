using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScooper : MonoBehaviour
{
    public bool bucket_wheel_turning;
    public Vector3 shrink = new Vector3(0.03f, 0.03f, 0.03f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);

    private void OnTriggerStay(Collider other)
    {
        if (bucket_wheel_turning)
        {
            if (other.gameObject.name == "dirt_ball")
            {
                other.gameObject.transform.localScale -= shrink;

                if (other.gameObject.transform.localScale.x < minimum_size.x &&
                    other.gameObject.transform.localScale.y < minimum_size.y &&
                    other.gameObject.transform.localScale.z < minimum_size.z)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
