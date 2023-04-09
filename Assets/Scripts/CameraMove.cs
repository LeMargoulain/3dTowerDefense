using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform myPlayerEyes;
    private Camera myCamera;
    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.position = myPlayerEyes.position;
        transform.rotation = myPlayerEyes.rotation;
    }
}
