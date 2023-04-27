using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSquisher : MonoBehaviour
{
    public Vector3 squish = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DirtBall")
        {
            other.gameObject.transform.localScale -= squish;

            if (other.gameObject.transform.localScale.x < minimum_size.x &&
                other.gameObject.transform.localScale.y < minimum_size.y &&
                other.gameObject.transform.localScale.z < minimum_size.z)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
