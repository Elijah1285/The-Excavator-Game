using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileBody;
    public float force = 50;
    public Vector3 forceDirection = new Vector3(-1, 0, 0);

    private void Start()
    {
        projectileBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyUp("f"))
        {
            projectileBody.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}
