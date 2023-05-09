using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public enum hAlignment {left, centre, right};
    public enum vAlignment {top, middle, bottom};
    public enum UnitsIn {pixels, screen_percentage};

    public hAlignment horizontal_alignment = hAlignment.left;
    public vAlignment vertical_alignment = vAlignment.top;
    public UnitsIn units = UnitsIn.screen_percentage;

    public Transform target;
    public Vector3 new_position;

    public int pip_width = 50;
    public int pip_height = 50;

    public int x_offset = 0;
    public int y_offset = 0;

    public bool update = true;
    private int horizontal_size = 0;
    private int vertical_size = 0;
    private int horizontal_location = 0;
    private int vertical_location = 0;

    private void Start()
    {
        GetComponent<Camera>().depth = 0;
        adjustCamera();
    }

    private void LateUpdate()
    {
        if (update)
        {
            adjustCamera();

            new_position = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = new_position; 
        }
    }

    void adjustCamera()
    {
        int screen_width = Screen.width;
        int screen_height = Screen.height;
        float screen_width_percent = screen_width * 0.01f;
        float screen_height_percent = screen_height * 0.01f;
        float x_off_percent = x_offset * screen_width_percent;
        float y_off_percent = y_offset * screen_height_percent;
        int x_off = 0;
        int y_off = 0;

        if (units == UnitsIn.screen_percentage)
        {
            horizontal_size = pip_width * (int)screen_width_percent;
            vertical_size = pip_height * (int)screen_height_percent;
            x_off = (int) x_off_percent;
            y_off = (int) y_off_percent;
        }
        else
        {
            horizontal_size = pip_width;
            vertical_size = pip_height;
            x_off = x_offset;
            y_off = y_offset;
        }

        switch (horizontal_alignment)
        {
            case hAlignment.left:
                horizontal_location = x_offset;
                break;
            case hAlignment.right:
                int justified_right = (screen_width - horizontal_size);
                horizontal_location = (justified_right - x_off);
                break;
            case hAlignment.centre:
                float justified_center = (screen_width * 0.5f) - (horizontal_size * 0.5f);
                horizontal_location = (int)(justified_center - x_off);
                break;
        }

        switch (vertical_alignment)
        {
            case vAlignment.top:
                int justified_top = screen_height - vertical_size;
                vertical_location = justified_top - y_off;
                break;
            case vAlignment.bottom:
                vertical_location = y_off;
                break;
            case vAlignment.middle:
                float justified_middle = (screen_height * 0.5f) - (vertical_size * 0.5f);
                vertical_location = (int)(justified_middle - y_off);
                break;
        }

        GetComponent<Camera>().pixelRect = new Rect(horizontal_location, vertical_location,
            horizontal_size, vertical_size);
    }
}
