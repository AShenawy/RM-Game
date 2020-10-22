using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attendants : MonoBehaviour
{
    public Attendant[] attendants;

    private void OnEnable()
    {
        CheckAttendants();

        if (GameManager.instance.currentTurn > 2)
            AttendClient();
    }

    private void Awake()
    {
        //CheckAttendants();
    }

    void CheckAttendants()
    {
        for (int i = 0; i < attendants.Length; i++)
        {
            if (!attendants[i].isAttending)
            {
                attendants[i].HideAttendee();
                attendants[i].enabled = false;
            }
            else
            {
                attendants[i].enabled = true;
                attendants[i].ShowAttendee();
            }
        }
    }

    void AttendClient()
    {
        for (int i = 0; i < attendants.Length; i++)
        {
            if (attendants[i].type == AttendantType.Client)
            {
                attendants[i].isAttending = true;
                attendants[i].ShowAttendee();
            }
        }
    }
}
