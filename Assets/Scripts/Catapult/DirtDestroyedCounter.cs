using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirtDestroyedCounter : MonoBehaviour
{
    public int dirt_destroyed_num = 0;

    public TMP_Text dirt_destroyed_x;

    public void incrementDirtDestroyedNum()
    {
        Debug.Log("before");
        Debug.Log(dirt_destroyed_num);
        dirt_destroyed_num++;
        Debug.Log("after");
        Debug.Log(dirt_destroyed_num);
        GetComponent<TMP_Text>().text = dirt_destroyed_num.ToString();

        if (!GetComponent<TMP_Text>().enabled && dirt_destroyed_num > 1)
        {
            GetComponent<TMP_Text>().enabled = true;
            dirt_destroyed_x.enabled = true;
        }
    }
}
