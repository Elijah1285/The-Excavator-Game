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
    public int speed_upgrade_cost = 100;
    public int capacity_upgrade_cost = 100;
    public int dirt_collection_upgrade_cost = 100;

    public TMP_Text speed_level_text;
    public TMP_Text capacity_level_text;
    public TMP_Text dirt_collection_level_text;
    public TMP_Text speed_upgrade_cost_text;
    public TMP_Text capacity_upgrade_cost_text;
    public TMP_Text dirt_collection_upgrade_cost_text;

    public Canvas upgrade_UI;

    public DirtDump dirt_dump;
    public DirtScooper dirt_scooper;
    public ExcavatorMovement excavator_movement;

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
                    if (dirt_dump.money >= speed_upgrade_cost)
                    {
                        speed_upgrade_flag = true;
                        speed_level++;
                        speed_level_text.text = speed_level.ToString();

                        excavator_movement.speed += 0.5f;

                        dirt_dump.money -= speed_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        speed_upgrade_cost += 100;
                        speed_upgrade_cost_text.text = speed_upgrade_cost.ToString();

                        GetComponent<AudioSource>().Play();
                    }
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
                    if (dirt_dump.money >= capacity_upgrade_cost)
                    {
                        capacity_upgrade_flag = true;
                        capacity_level++;
                        capacity_level_text.text = capacity_level.ToString();

                        dirt_scooper.dirt_capacity += 500;

                        dirt_dump.money -= capacity_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        capacity_upgrade_cost += 100;
                        capacity_upgrade_cost_text.text = capacity_upgrade_cost.ToString();

                        GetComponent<AudioSource>().Play();
                    }
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
                    if (dirt_dump.money > dirt_collection_upgrade_cost)
                    {
                        dirt_collection_upgrade_flag = true;
                        dirt_collection_level++;
                        dirt_collection_level_text.text = dirt_collection_level.ToString();

                        dirt_dump.money -= dirt_collection_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        dirt_collection_upgrade_cost += 100;
                        dirt_collection_upgrade_cost_text.text = dirt_collection_upgrade_cost.ToString();

                        GetComponent<AudioSource>().Play();
                    }
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
