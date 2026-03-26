using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepManager : MonoBehaviour
{

    public Transform[] foot_children;

    //Events
    void OnEnable()
    {
        ClueManager.OnClueCompleted += HandleClueCompleted;
    }

    void OnDisable()
    {
        ClueManager.OnClueCompleted -= HandleClueCompleted;
    }

    void Start()
    {
    }
    

    //Helper function to show the staged progression pattern
    void HandleClueCompleted(int index)
    {
        if (index < 0 || index >= foot_children.Length) return;

        //Set active only the next clue footsteps
        foreach (Transform t in foot_children)
        {
            t.gameObject.SetActive(false);
        }

        foot_children[index].gameObject.SetActive(true);
    }
}
