using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activities : MonoBehaviour
{
    public Activity[] activities;

    private void Awake()
    {
        for (int i = 0; i < activities.Length; i++)
        {
            if (!activities[i].isHappening)
            {

            }
        }
    }
}
