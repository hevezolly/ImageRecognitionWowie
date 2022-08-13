using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassCameraRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation;
    }
}
