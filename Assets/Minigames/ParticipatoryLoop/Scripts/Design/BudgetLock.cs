using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetLock : MonoBehaviour
{
    public Toggle[] attendantsToggles;
    public Toggle[] activitiesToggles;

    public BudgetCounter budgeter;
    public Text errorText;

    private bool isAttendantMissing;
    private bool isActivityMissing;


    private void OnEnable()
    {
        budgeter.onBudgetChanged += CheckErrors;
    }

    private void OnDisable()
    {
        budgeter.onBudgetChanged -= CheckErrors;
    }

    private void Start()
    {
        CheckAttendants();
        CheckActivities();
    }

    public void CheckAttendants()
    {
        for (int i = 0; i < attendantsToggles.Length; i++)
        {
            if (attendantsToggles[i].isOn)
            {
                isAttendantMissing = false;
                CheckErrors();
                return;
            }
        }

            isAttendantMissing = true;
            CheckErrors();
    }

    public void CheckActivities()
    {
        for (int i = 0; i < activitiesToggles.Length; i++)
        {
            if (activitiesToggles[i].isOn)
            {
                isActivityMissing = false;
                CheckErrors();
                return;
            }
        }

            isActivityMissing = true;
            CheckErrors();
    }

    void CheckErrors()
    {
        //if (budgeter.currentBudget < 0)
        //{
        //    GetComponent<Button>().interactable = false;
        //    errorText.text = "Budget cannot be under 0";
        //    return;
        //}
        
        if (isAttendantMissing)
        {
            GetComponent<Button>().interactable = false;
            errorText.text = "At least 1 attendant must be chosen";
            return;
        }

        //if (isActivityMissing)
        //{
        //    GetComponent<Button>().interactable = false;
        //    errorText.text = "At least 1 activity must be chosen";
        //    return;
        //}

        GetComponent<Button>().interactable = true;
        errorText.text = "";
    }
}
