using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtScooper : MonoBehaviour
{
    public bool bucket_wheel_turning;
    public Vector3 shrink = new Vector3(0.03f, 0.03f, 0.03f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 size_for_rigidbody = new Vector3(5.0f, 5.0f, 5.0f);
    public int dirt_counter = 0;
    public int dirt_intersected = 0;
    public int dirt_capacity = 1000;
    public TMP_Text dirt_counter_text;

    private void Update()
    {
        if (dirt_intersected > 0 && bucket_wheel_turning && dirt_counter < dirt_capacity)
        {
            if (GetComponent<AudioSource>().volume < 1.0f)
            {
                GetComponent<AudioSource>().volume = 1.0f;
            }
        }
        else
        {
            if (GetComponent<AudioSource>().volume > 0.0f)
            {
                GetComponent<AudioSource>().volume = 0.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (bucket_wheel_turning)
        {
            if (other.gameObject.tag == "DirtBall")
            {
                if (dirt_counter < dirt_capacity)
                {
                    other.gameObject.transform.localScale -= shrink;
                    dirt_counter++;
                    dirt_counter_text.text = dirt_counter.ToString();

                    if (other.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        other.gameObject.GetComponent<Rigidbody>().mass = other.gameObject.transform.localScale.x * 100;
                    }

                    if (other.gameObject.transform.localScale.x < size_for_rigidbody.x &&
                        other.gameObject.transform.localScale.y < size_for_rigidbody.y &&
                        other.gameObject.transform.localScale.z < size_for_rigidbody.z)
                    {
                        if (other.gameObject.GetComponent<Rigidbody>() == null)
                        {
                            other.gameObject.AddComponent(typeof(Rigidbody));
                            other.gameObject.GetComponent<Rigidbody>().drag = 1.0f;
                            other.gameObject.GetComponent<Rigidbody>().angularDrag = 1.0f;

                            if (dirt_intersected > 0)
                            {
                                dirt_intersected--;
                            }
                        }
                    }

                    if (other.gameObject.transform.localScale.x < minimum_size.x &&
                        other.gameObject.transform.localScale.y < minimum_size.y &&
                        other.gameObject.transform.localScale.z < minimum_size.z)
                    {
                        Destroy(other.gameObject);

                        if (dirt_intersected > 0)
                        {
                            dirt_intersected--;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DirtBall")
        {
            dirt_intersected++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DirtBall")
        {
            dirt_intersected--;
        }
    }
}