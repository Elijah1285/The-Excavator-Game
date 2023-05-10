using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launch : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce = 800.0f;
    GameObject missile;
    public bool missile_is_live = false;
    public bool player_in_range = false;

    public TMP_Text catapult_instruction;

    public Camera projectile_cam;
    public Camera player_cam;

    void FixedUpdate()
    {
        bool fire = Input.GetButton("Fire");
        bool instantiate = Input.GetButton("Instantiate");

        if (instantiate && (!missile_is_live) && player_in_range)
        {
            missile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().useGravity = false;
            missile_is_live = true;
        }
        if (fire && (missile_is_live) && player_in_range)
        {
            projectile_cam.transform.parent = missile.transform;

            player_cam.enabled = false;
            projectile_cam.enabled = true;

            missile.GetComponent<Rigidbody>().AddForce(missile.transform.right * projectileForce, ForceMode.Acceleration);
            missile.GetComponent<Rigidbody>().useGravity = true;
            missile_is_live = false;

            if (catapult_instruction.enabled)
            {
                catapult_instruction.enabled = false;
            }
        }
    }
}
