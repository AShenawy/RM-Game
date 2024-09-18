using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectActivator : MonoBehaviour
{
    public static event System.Action OnGameObjectActivated = delegate { };
    public static event System.Action OnGameObjectDeactivated = delegate { };
    [SerializeField] private Button previousButton;

    private void Start()
    {
        if (previousButton!=null)
        {
            previousButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        OnGameObjectActivated?.Invoke();
    }

    private void OnDisable()
    {
        OnGameObjectDeactivated?.Invoke();
    }
}

