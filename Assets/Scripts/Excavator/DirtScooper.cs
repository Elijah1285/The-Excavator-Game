using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScooper : MonoBehaviour
{
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision");
    }
}
