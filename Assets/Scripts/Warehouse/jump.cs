using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float force = 1.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyUp("j"))
        {
            rb.AddExplosionForce(force, this.transform.position, 1.0f, 1.0f, ForceMode.Impulse);
        }
    }
}
