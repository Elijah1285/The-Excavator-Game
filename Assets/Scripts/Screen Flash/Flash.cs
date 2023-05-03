using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Flash : MonoBehaviour
{
    Image image = null;

    Coroutine current_flash_routine = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void startFlash(float seconds_for_one_flash, float max_alpha, Color new_colour)
    {
        image.color = new_colour;

        // ensure max alpha isn't above 1
        max_alpha = Mathf.Clamp(max_alpha, 0, 1);

        if (current_flash_routine != null)
        {
            StopCoroutine(current_flash_routine);
        }

        current_flash_routine = StartCoroutine(flash(seconds_for_one_flash, max_alpha));
    }

    IEnumerator flash(float seconds_for_one_flash, float max_alpha)
    {
        // animate flash in
        float flash_in_duration = seconds_for_one_flash / 2;

        for (float t = 0; t <= flash_in_duration; t += Time.deltaTime)
        {
            Color colour_this_frame = image.color;
            colour_this_frame.a = Mathf.Lerp(0, max_alpha, t / flash_in_duration);
            image.color = colour_this_frame;
            yield return null;
        }

        // animate flash out
        float flash_out_duration = seconds_for_one_flash / 2;

        for (float t = 0; t <= flash_out_duration; t += Time.deltaTime)
        {
            Color colour_this_frame = image.color;
            colour_this_frame.a = Mathf.Lerp(max_alpha, 0, t / flash_out_duration);
            image.color = colour_this_frame;
            yield return null;
        }

        // ensure alpha is set to 0
        image.color = new Color32(0, 0, 0, 0);
    }
}
