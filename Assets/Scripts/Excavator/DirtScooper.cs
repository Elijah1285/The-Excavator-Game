using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtScooper : MonoBehaviour
{
    public bool bucket_wheel_turning;
    public Vector3 shrink = new Vector3(0.03f, 0.03f, 0.03f);
    public Vector3 minimum_size = new Vector3(0.5f, 0.5f, 0.5f);
    public int dirt_counter = 0;
    public TMP_Text dirt_counter_text;

    private void OnTriggerStay(Collider other)
    {
        if (bucket_wheel_turning)
        {
            if (other.gameObject.tag == "DirtBall")
            {
                other.gameObject.transform.localScale -= shrink;
                dirt_counter++;
                dirt_counter_text.text = dirt_counter.ToString();
                Debug.Log(dirt_counter);

                if (other.gameObject.transform.localScale.x < minimum_size.x &&
                    other.gameObject.transform.localScale.y < minimum_size.y &&
                    other.gameObject.transform.localScale.z < minimum_size.z)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
