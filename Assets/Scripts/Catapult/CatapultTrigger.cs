using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatapultTrigger : MonoBehaviour
{
    public bool shown_catapult_instruction = false;
    public TMP_Text catapult_instruction;

    public Launch launch;
    public EnterTheExcavator enter_the_excavator;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !enter_the_excavator.player_in_excavator)
        {
            launch.player_in_range = true;

            if (!shown_catapult_instruction)
            {
                catapult_instruction.enabled = true;
                shown_catapult_instruction= true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            launch.player_in_range = false;
        }
    }
}
