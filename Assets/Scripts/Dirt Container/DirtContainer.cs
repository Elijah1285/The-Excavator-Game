using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtContainer : MonoBehaviour
{
    public float dirt_counter = 0;
    public int dirt_capacity = 1000;
    public bool dumping = false;

    void FixedUpdate()
    {
        if (!dumping && dirt_counter > 0)
        {
            dirt_counter -= 0.05f;

            if (dirt_counter < 0)
            {
                dirt_counter = 0;
            }
        }
    }
}
