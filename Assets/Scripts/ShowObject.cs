using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{
    public void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
