using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtDestroyedTextTimer : MonoBehaviour
{
    public bool timer_running = false;
    public bool fading_out = false;
    public float timer = 3.0f;

    public Color colour;
    public Color original_colour;

    public TMP_Text dirt_destroyed_counter;
    public TMP_Text dirt_destroyed_x;

    private void Start()
    {
        colour = GetComponent<TMP_Text>().color;
        original_colour = colour;
    }

    private void Update()
    {
        if (timer_running)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fading_out = true;
                timer_running = false;
                timer = 3.0f;
            }
        }

        if (fading_out)
        {
            fadeOut();
        }
    }

    private void fadeOut()
    {
        colour.a -= Time.deltaTime;
        GetComponent<TMP_Text>().color = colour;
        dirt_destroyed_counter.color = colour;
        dirt_destroyed_x.color = colour;

        if (colour.a <= 0)
        {
            GetComponent<TMP_Text>().color = original_colour;
            dirt_destroyed_counter.color = original_colour;
            dirt_destroyed_x.color = original_colour;
            colour = original_colour;
            dirt_destroyed_counter.GetComponent<DirtDestroyedCounter>().dirt_destroyed_num = 0;
            dirt_destroyed_counter.text = "0";
            GetComponent<TMP_Text>().enabled = false;
            dirt_destroyed_counter.enabled = false;
            dirt_destroyed_x.enabled = false;
            fading_out = false;
        }
    }
}
