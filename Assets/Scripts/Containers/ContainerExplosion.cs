using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerExplosion : MonoBehaviour
{
    public float power = 10.0f;
    public float radius = 5.0f;
    public float upwards_modifier = 1.0f;

    private void Update()
    {
        if (Input.GetKeyUp("c"))
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(power, this.transform.position, radius, upwards_modifier, ForceMode.Impulse);
                }
            }
        }
    }
}
