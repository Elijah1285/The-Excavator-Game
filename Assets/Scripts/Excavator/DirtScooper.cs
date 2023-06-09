using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtScooper : MonoBehaviour
{
    public bool bucket_wheel_turning;
    public bool shown_dirt_container_nav_instruction = false;
    public Vector3 shrink = new Vector3(0.03f, 0.03f, 0.03f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 size_for_rigidbody = new Vector3(5.0f, 5.0f, 5.0f);
    public int dirt_counter = 0;
    public int dirt_intersected = 0;
    public int dirt_capacity = 1000;
    public int dirt_particle_type = 1;
    public int dirt_level = 1;
    public int diggable_dirt = 1;
    public float digging_speed = 1.0f;
    public TMP_Text dirt_counter_text;
    public TMP_Text full_text;
    public TMP_Text dirt_container_nav_instruction;
    public ParticleSystem dirt_particles;
    public Material dirt_1;
    public Material dirt_2;
    public Material dirt_3;

    private void Update()
    {
        if (dirt_intersected > 0 && bucket_wheel_turning && dirt_counter < dirt_capacity && (int)digging_speed >= dirt_level)
        {
            if (GetComponent<AudioSource>().volume < 1.0f)
            {
                GetComponent<AudioSource>().volume = 1.0f;
            }

            if (!dirt_particles.isEmitting)
            {
                dirt_particles.Play();
            }
        }
        else
        {
            if (GetComponent<AudioSource>().volume > 0.0f)
            {
                GetComponent<AudioSource>().volume = 0.0f;
            }

            if (dirt_particles.isEmitting)
            {
                dirt_particles.Stop();
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
                    other.gameObject.transform.localScale -= shrink * digging_speed;
                    dirt_counter += (int) digging_speed;

                    if (dirt_particle_type != 1)
                    {
                        dirt_particles.GetComponent<Renderer>().material = dirt_1;
                        dirt_particle_type = 1;
                    }

                    if (dirt_counter > dirt_capacity)
                    {
                        dirt_counter = dirt_capacity;
                    }

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

                    if (dirt_counter >= dirt_capacity)
                    {
                        full_text.enabled = true;

                        if (!shown_dirt_container_nav_instruction)
                        {
                            dirt_container_nav_instruction.enabled = true;
                            shown_dirt_container_nav_instruction = true;
                        }
                    }
                }
            }
            else if (other.gameObject.tag == "DirtBall2" && digging_speed >= 2.0f)
            {
                if (dirt_counter < dirt_capacity)
                {
                    other.gameObject.transform.localScale -= shrink * digging_speed * 0.5f;
                    dirt_counter += (int)digging_speed;

                    if (dirt_particle_type != 2)
                    {
                        dirt_particles.GetComponent<Renderer>().material = dirt_2;
                        dirt_particle_type = 2;
                    }

                    if (dirt_counter > dirt_capacity)
                    {
                        dirt_counter = dirt_capacity;
                    }

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

                    if (dirt_counter >= dirt_capacity)
                    {
                        full_text.enabled = true;
                    }
                }
            }
            else if (other.gameObject.tag == "DirtBall3" && digging_speed >= 3.0f)
            {
                if (dirt_counter < dirt_capacity)
                {
                    other.gameObject.transform.localScale -= shrink * digging_speed * 0.25f;
                    dirt_counter += (int)digging_speed;

                    if (dirt_particle_type != 3)
                    {
                        dirt_particles.GetComponent<Renderer>().material = dirt_3;
                        dirt_particle_type = 3;
                    }

                    if (dirt_counter > dirt_capacity)
                    {
                        dirt_counter = dirt_capacity;
                    }

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

                    if (dirt_counter >= dirt_capacity)
                    {
                        full_text.enabled = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DirtBall")
        {
            dirt_level = 1;
            dirt_intersected++;
        }
        else if (other.gameObject.tag == "DirtBall2")
        {
            dirt_level = 2;
            dirt_intersected++;
        }
        else if (other.gameObject.tag == "DirtBall3")
        {
            dirt_level = 3;
            dirt_intersected++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DirtBall" || other.gameObject.tag == "DirtBall2" || other.gameObject.tag == "DirtBall3")
        {
            dirt_intersected--;
        }
    }
}
