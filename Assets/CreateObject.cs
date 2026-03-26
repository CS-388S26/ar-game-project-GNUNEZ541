using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public Camera targetCamera;   // Assign in Inspector
    public GameObject prefab;
    public float spawnDistance = 5f;

    public void Spawn()
    {

        GameObject existing = GameObject.FindGameObjectWithTag("magnifying");

        // Only spawn if none exists
        if (existing == null)
        {
            GameObject obj = Instantiate(prefab, targetCamera.transform);

            obj.transform.localPosition = new Vector3(0, 0, spawnDistance);
            Vector3 myRotation = new Vector3(55.868f, 270f, 90f);
            obj.transform.localEulerAngles = myRotation;
            
        }

       
    }

}

