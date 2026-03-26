using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClueManager : MonoBehaviour
{
    public float focus_time = 5f; 
    private Transform[] children;
    private float[] timers; 
    private TMP_Text[] texts;

    private int current_focused = -1;

    public static event Action<int> OnClueCompleted;

    //Events
    void OnEnable()
    {
        InvestigateClue.OnClueHit += HandleClueProgress;
    }

    void OnDisable()
    {
        InvestigateClue.OnClueHit -= HandleClueProgress;
    }

    void Start()
    {
        //Save necessary information to show clue progress
        children = new Transform[transform.childCount];
        timers = new float[children.Length];
        texts = new TMP_Text[children.Length];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            children[i].gameObject.SetActive(false);

            timers[i] = 0f;

            TMP_Text[] allTexts = children[i].GetComponentsInChildren<TMP_Text>();

            //To show the percentages
            foreach (TMP_Text t in allTexts)
            {
                if (t.gameObject.name == "Percentage")
                {
                    texts[i] = t;
                    texts[i].text = "0%";
                    break;
                }
            }
        }
    }

    void Update()
    {
        //If there is a clue being investigated currently
        if (current_focused != -1)
        {
            timers[current_focused] += Time.deltaTime;

            //Update the progress text
            float percent = Mathf.Clamp01(timers[current_focused] / focus_time);
            int percentInt = Mathf.RoundToInt(percent * 100);

            if (texts[current_focused] != null)
                texts[current_focused].text = percentInt + "%";

            //If it reaches 100% then activate staged progression for the next clue
            if (timers[current_focused] >= focus_time)
            {
                Renderer rend = children[current_focused].GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.color = Color.green; 
                }

                OnClueCompleted?.Invoke(current_focused);
            }

            current_focused = -1;
        }
        else
        {
            current_focused = -1;
        }
    }

    //To update the progress depending on the clue
    void HandleClueProgress(string clue_name)
    {
        switch (clue_name)
        {
            case "Clue1":
                ActivateChild(0);
                break;
            case "Clue2":
                ActivateChild(1);
                break;
            case "Clue3":
                ActivateChild(2);
                break;
            default:
                Debug.LogWarning("No child mapped for clue: " + clue_name);
                break;
        }
    }

    void ActivateChild(int index)
    {
        if (index < 0 || index >= children.Length) return;

        int prev_index = index - 1;

        if(prev_index > -1 && timers[prev_index] < focus_time)
            return;

        current_focused = index;

        if (!children[index].gameObject.activeSelf)
            children[index].gameObject.SetActive(true);
    }
}
