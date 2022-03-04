using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public int tDelay = 1;
    void Start()
    {
        Destroy(gameObject, tDelay);
    }
}
