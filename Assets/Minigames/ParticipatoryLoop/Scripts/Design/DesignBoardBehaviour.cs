using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignBoardBehaviour : MonoBehaviour
{
    public Toggle[] attendantToggles;
    public Toggle[] activityToggles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetToggles()
    {
        for (int i = 0; i < attendantToggles.Length; i++)
        {
            attendantToggles[i].isOn = false;
        }

        //for (int j = 0; j < activityToggles.Length; j++)
        //{
        //    activityToggles[j].isOn = false;
        //}
    }
}
