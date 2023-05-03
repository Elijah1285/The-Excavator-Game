using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSquisher : MonoBehaviour
{
    public Vector3 squish = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 size_for_rigidbody = new Vector3(5.0f, 5.0f, 5.0f);

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DirtBall" || other.gameObject.tag == "DirtBall2" || other.gameObject.tag == "DirtBall3")
        {
            other.gameObject.transform.localScale -= squish;

            if (other.gameObject.transform.localScale.x < size_for_rigidbody.x &&
                       other.gameObject.transform.localScale.y < size_for_rigidbody.y &&
                       other.gameObject.transform.localScale.z < size_for_rigidbody.z)
            {
                if (other.gameObject.GetComponent<Rigidbody>() == null)
                {
                    other.gameObject.AddComponent(typeof(Rigidbody));
                    other.gameObject.GetComponent<Rigidbody>().drag = 1.0f;
                    other.gameObject.GetComponent<Rigidbody>().angularDrag = 1.0f;
                }
            }

            if (other.gameObject.transform.localScale.x < minimum_size.x &&
                other.gameObject.transform.localScale.y < minimum_size.y &&
                other.gameObject.transform.localScale.z < minimum_size.z)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
