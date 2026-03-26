using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvestigateClue : MonoBehaviour
{
    private Camera targetCamera;  
    public float rayDistance = 10f;

    private Renderer rend;

    public static event Action<string> OnClueHit;

    void Start()
    {
        rend = GetComponent<Renderer>();

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        if (camObj != null)
        {
            targetCamera = camObj.GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;

            rend.material.color = Color.green;

            if (Physics.Raycast(transform.position, targetCamera.transform.forward, out hit, rayDistance))
            {
                rend.material.color = Color.blue;

                if (hit.collider.CompareTag("Clue"))
                {
                    OnClueHit?.Invoke(hit.collider.name);
                }
            }

        }
        else
        {
            rend.material.color = Color.red;
        }
    }
}
