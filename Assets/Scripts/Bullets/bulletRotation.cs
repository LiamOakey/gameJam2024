using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletRotation : MonoBehaviour
{
    public float rotationSpeed = 4f;

    void FixedUpdate()
    {
        transform.Rotate(0,0,rotationSpeed);
    }
}
