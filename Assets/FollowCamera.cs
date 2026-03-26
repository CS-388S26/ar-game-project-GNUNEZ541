using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera targetCamera;
    public float distanceFromCamera = 5f;

    void LateUpdate()
    {
        if (targetCamera == null) return;

        transform.position = targetCamera.transform.position +
                             targetCamera.transform.forward * distanceFromCamera;
    }
}
