using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsInstructionToggle : MonoBehaviour
{
    public bool toggled_controls = false;

    void Update()
    {
        bool show_hide_controls = Input.GetButton("ShowHideControls");

        if (show_hide_controls && !toggled_controls)
        {
            GetComponent<TMP_Text>().enabled = !GetComponent<TMP_Text>().enabled;
            toggled_controls = true;
        }
        else if (!show_hide_controls && toggled_controls)
        {
            toggled_controls = false;
        }
    }
}
