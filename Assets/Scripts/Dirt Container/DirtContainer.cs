using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtContainer : MonoBehaviour
{
    public float dirt_counter = 0;
    public int dirt_capacity = 1000;
    public bool dumping = false;
    public bool update_dirt = false;
    public GameObject dirt;

    void FixedUpdate()
    {
        if (!dumping && dirt_counter > 0)
        {
            dirt_counter -= 0.05f;

            if (dirt_counter < 0)
            {
                dirt_counter = 0;
            }

            updateDirt();
        }
    }

    public void updateDirt()
    {
        if (dirt_counter > 0)
        {
            dirt.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            dirt.GetComponent<MeshRenderer>().enabled = false;
        }

        Vector3 new_scale = new Vector3(dirt_counter * 0.012f, dirt.transform.localScale.y, dirt.transform.localScale.z);
        dirt.transform.localScale = new_scale;
    }
}
