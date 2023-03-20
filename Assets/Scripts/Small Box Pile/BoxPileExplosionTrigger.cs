using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPileExplosionTrigger : MonoBehaviour
{
    public float explosion_force = 10.0f;
    public float explosion_radius = 5.0f;
    public float upwards_modifier = 1.0f;
    public GameObject trigger_ball;

    private void OnTriggerEnter(Collider other)
    {
        Collider TriggerBallCollider = trigger_ball.GetComponent<Collider>();

        if (other == TriggerBallCollider)
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
