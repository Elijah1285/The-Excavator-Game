using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    public float explosion_force = 10.0f;
    public float explosion_radius = 5.0f;
    public float upwards_modifier = 1.0f;

    private void Update()
    {
        if (Input.GetKeyUp("e"))
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosion_radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(explosion_force, this.transform.position, explosion_radius, upwards_modifier, ForceMode.Impulse);
                }
            }
        }
    }
}
