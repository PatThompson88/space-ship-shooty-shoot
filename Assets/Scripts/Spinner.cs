using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinSpeed = 180f; //quaternion angle / second
    private void SpinObject(){
        transform.Rotate(0, 0, (spinSpeed * Time.deltaTime));
    }

    // Update is called once per frame
    void Update()
    {
        SpinObject();
    }
}
