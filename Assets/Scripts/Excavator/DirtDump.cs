using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtDump : MonoBehaviour
{
    public int money = 0;

    public GameObject current_dump;
    public DirtScooper dirt_scooper;
    public TMP_Text full_text;
    public TMP_Text money_counter;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DirtContainer")
        {
            current_dump = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DirtContainer")
        {
            current_dump.GetComponent<DirtContainer>().dumping = false;
            current_dump = null;
        }
    }

    void Update()
    {
        float dump = Input.GetAxis("Dump");

        if (dump > 0)
        {
            if (current_dump != null)
            {
                if (dirt_scooper.dirt_counter > 0 && current_dump.GetComponent<DirtContainer>().dirt_counter < current_dump.GetComponent<DirtContainer>().dirt_capacity)
                {
                    dirt_scooper.dirt_counter--;
                    dirt_scooper.dirt_counter_text.text = dirt_scooper.dirt_counter.ToString();
                    current_dump.GetComponent<DirtContainer>().dirt_counter++;
                    money++;
                    money_counter.text = money.ToString();

                    if (current_dump.GetComponent<DirtContainer>().dirt_counter > current_dump.GetComponent<DirtContainer>().dirt_capacity)
                    {
                        current_dump.GetComponent<DirtContainer>().dirt_counter = current_dump.GetComponent<DirtContainer>().dirt_capacity;
                    }

                    current_dump.GetComponent<DirtContainer>().updateDirt();

                    if (!current_dump.GetComponent<DirtContainer>().dumping)
                    {
                        current_dump.GetComponent<DirtContainer>().dumping = true;
                    }

                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                    }

                    if ((current_dump.GetComponent<DirtContainer>().dirt_counter >= current_dump.GetComponent<DirtContainer>().dirt_capacity ||
                        dirt_scooper.dirt_counter <= 0) && GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Stop();
                    }

                    if (full_text.enabled)
                    {
                        full_text.enabled = false;
                    }
                }
            }
        }
        else
        {
            if (current_dump != null)
            {
                if (current_dump.GetComponent<DirtContainer>().dumping)
                {
                    current_dump.GetComponent<DirtContainer>().dumping = false;
                }

                if (GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Stop();
                }
            }
        }
    }
}
