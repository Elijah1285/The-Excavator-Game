using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public bool speed_upgrade_flag = false;
    public bool capacity_upgrade_flag = false;
    public bool digging_upgrade_flag = false;
    public bool in_upgrade_station = false;

    public int speed_level = 1;
    public int capacity_level = 1;
    public int digging_level = 1;
    public int speed_upgrade_cost = 100;
    public int capacity_upgrade_cost = 100;
    public int digging_upgrade_cost = 100;

    public int max_level = 10;

    public TMP_Text speed_level_text;
    public TMP_Text capacity_level_text;
    public TMP_Text digging_level_text;
    public TMP_Text speed_upgrade_cost_text;
    public TMP_Text capacity_upgrade_cost_text;
    public TMP_Text digging_upgrade_cost_text;

    public Canvas upgrade_UI;

    public DirtDump dirt_dump;
    public DirtScooper dirt_scooper;
    public ExcavatorMovement excavator_movement;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation")
        {
            upgrade_UI.enabled = true;
            in_upgrade_station = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation")
        {
            upgrade_UI.enabled = false;
            in_upgrade_station = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "UpgradeStation" && excavator_movement.is_playing)
        {
            float speed_upgrade = Input.GetAxis("SpeedUpgrade");
            float capacity_upgrade = Input.GetAxis("CapacityUpgrade");
            float digging_upgrade = Input.GetAxis("DiggingUpgrade");

            if (speed_upgrade > 0)
            {
                if (!speed_upgrade_flag)
                {
                    if (dirt_dump.money >= speed_upgrade_cost && speed_level < max_level)
                    {
                        speed_upgrade_flag = true;
                        speed_level++;
                        speed_level_text.text = speed_level.ToString();

                        excavator_movement.speed += 0.5f;

                        dirt_dump.money -= speed_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        speed_upgrade_cost += 100;

                        if (speed_level < max_level)
                        {
                            speed_upgrade_cost_text.text = speed_upgrade_cost.ToString();
                        }
                        else
                        {
                            speed_upgrade_cost_text.text = "MAX";
                        }

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
                    if (dirt_dump.money >= capacity_upgrade_cost && capacity_level < max_level)
                    {
                        capacity_upgrade_flag = true;
                        capacity_level++;
                        capacity_level_text.text = capacity_level.ToString();

                        dirt_scooper.dirt_capacity += 500;

                        dirt_dump.money -= capacity_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        capacity_upgrade_cost += 100;

                        if (capacity_level < max_level)
                        {
                            capacity_upgrade_cost_text.text = capacity_upgrade_cost.ToString();
                        }
                        else
                        {
                            capacity_upgrade_cost_text.text = "MAX";
                        }

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

            if (digging_upgrade > 0)
            {
                if (!digging_upgrade_flag)
                {
                    if (dirt_dump.money >= digging_upgrade_cost && digging_level < max_level)
                    {
                        digging_upgrade_flag = true;
                        digging_level++;
                        digging_level_text.text = digging_level.ToString();

                        dirt_scooper.digging_speed += 0.5f;

                        dirt_dump.money -= digging_upgrade_cost;
                        dirt_dump.money_counter.text = dirt_dump.money.ToString();

                        digging_upgrade_cost += 100;

                        if (digging_level < max_level)
                        {
                            digging_upgrade_cost_text.text = digging_upgrade_cost.ToString();
                        }
                        else
                        {
                            digging_upgrade_cost_text.text = "MAX";
                        }

                        GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                if (digging_upgrade_flag)
                {
                    digging_upgrade_flag = false;
                }
            }
        }
    }
}
