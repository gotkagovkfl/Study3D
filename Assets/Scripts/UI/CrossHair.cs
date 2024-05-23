using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Vector3 pos {get; private set;}

    // Update is called once per frame
    void LateUpdate()
    {
        pos = Input.mousePosition;
        transform.position = pos;
    }
}
