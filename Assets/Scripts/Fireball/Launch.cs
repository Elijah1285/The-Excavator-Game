using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce = 800.0f;
    GameObject missile;
    public bool missile_is_live;

    void FixedUpdate()
    {
        bool fire = Input.GetButton("Fire");
        bool instantiate = Input.GetButton("Instantiate");

        if (instantiate && (!missile_is_live))
        {
            missile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().useGravity = false;
            missile_is_live = true;
        }
        if (fire && (missile_is_live))
        {
            missile.GetComponent<Rigidbody>().AddForce(missile.transform.right * projectileForce, ForceMode.Acceleration);
            missile.GetComponent<Rigidbody>().useGravity = true;
            missile_is_live = false;
        }
    }
}
