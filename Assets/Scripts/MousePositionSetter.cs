using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionSetter : MonoBehaviour
{
    [SerializeField]
    private InputPointer position;
    [SerializeField]
    private Camera cam;

    // Update is called once per frame
    void Update()
    {
        position.UpdatePosition(cam.ScreenToWorldPoint(Input.mousePosition));
    }
}
