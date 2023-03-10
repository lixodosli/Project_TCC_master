using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstigatorPointCamera : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}