using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    public static event System.Action OnGameObjectActivated = delegate { };
    public static event System.Action OnGameObjectDeactivated = delegate { };

    private void OnEnable()
    {
        OnGameObjectActivated?.Invoke();
    }

    private void OnDisable()
    {
        OnGameObjectDeactivated?.Invoke();
    }
}

