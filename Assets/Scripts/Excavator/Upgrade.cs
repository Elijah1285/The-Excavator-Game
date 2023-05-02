using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public bool speed_upgrade_flag = false;
    public bool capacity_upgrade_flag = false;
    public bool dirt_collection_upgrade_flag = false;

    public int speed_level = 1;
    public int capacity_level = 1;
    public int dirt_collection_level = 1;

    public TMP_Text speed_level_text;
    public TMP_Text capacity_level_text;
    public TMP_Text dirt_collection_level_text;

    public Canvas upgrade_UI;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation")
        {
            upgrade_UI.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation")
        {
            upgrade_UI.enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation")
        {
            float speed_upgrade = Input.GetAxis("SpeedUpgrade");
            float capacity_upgrade = Input.GetAxis("CapacityUpgrade");
            float dirt_collection_upgrade = Input.GetAxis("DirtCollectionUpgrade");

            if (speed_upgrade > 0)
            {
                if (!speed_upgrade_flag)
                {
                    speed_upgrade_flag = true;
                    speed_level++;
                    speed_level_text.text = speed_level.ToString();
                }
            }
            else
            {
                if (speed_upgrade_flag)
                {
                    speed_upgrade_flag = false;
                }
            }
            
            if (capacity_upgrade > 0)
            {
                if (!capacity_upgrade_flag)
                {
                    capacity_upgrade_flag = true;
                    capacity_level++;
                    capacity_level_text.text = capacity_level.ToString();
                }             
            }
            else
            {
                if (capacity_upgrade_flag)
                {
                    capacity_upgrade_flag = false;
                }
            }

            if (dirt_collection_upgrade > 0)
            {
                if (!dirt_collection_upgrade_flag)
                {
                    dirt_collection_upgrade_flag = true;
                    dirt_collection_level++;
                    dirt_collection_level_text.text = dirt_collection_level.ToString();
                }
            }
            else
            {
                if (dirt_collection_upgrade_flag)
                {
                    dirt_collection_upgrade_flag = false;
                }
            }
        }
    }
}
