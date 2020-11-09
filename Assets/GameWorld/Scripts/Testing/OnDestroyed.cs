using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyed : MonoBehaviour
{
    public void OnClicked()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        print(name + " destroyed");
    }
}
