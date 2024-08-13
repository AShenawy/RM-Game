using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script tests 2 ways to assign list values from one list to another.
 * using the assignment operator (=) doesn't copy the values of a list. Instead it copies references
 * to the other list. Hence, if the original list is cleared or modified, all lists assigned from it
 * are also changed.
 * If we want to copy a list to a new list and use each independently, then one method is to
 * create a new List object and initialise it with the list we want to copy. This is done in first case.
 */

public class ListTesting : MonoBehaviour
{
    public List<int> firstList = new List<int>();
    public List<int> secondList;    // we don't assign new list as it will be replaced with new List object in first test case below


    // Update is called once per frame
    void Update()
    {
        // in this case we create a new List object and populate it with firstList's elements
        if (Input.GetKeyDown(KeyCode.D))
        {
            secondList = new List<int>(firstList);
            firstList.Clear();

            // firstList will be emptied. However, secondList will keep the elements since they're copied.
        }

        // in this case assign secondList the value of firstList's elements
        if (Input.GetKeyDown(KeyCode.C))
        {
            secondList = firstList;
            firstList.Clear();

            // both firstList & secondList will be cleared. Since secondList was assigned by reference to firstList's elements.
        }
    }
}