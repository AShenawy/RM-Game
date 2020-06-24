using Doozy.Engine.UI;
using System;
using UnityEngine;

public class Service : MonoBehaviour
{
    public string Name;
    public VisitorType Type;
    public bool IsLocated;

    UIView uiView;
    RectTransform currentLocation;

    public static event Action<Service> OnLocated = delegate { };

    void Awake() => uiView = GetComponent<UIView>();

    public void Locate(RectTransform location)
    {
        SetLocationAndVisibility(location);
        OnLocated?.Invoke(this);
    }

    void SetLocationAndVisibility(RectTransform location)
    {
        if (currentLocation != location)
        {
            if (currentLocation != null)
                currentLocation.gameObject.SetActive(true);

            currentLocation = location;
        }
        uiView.Hide(true);
        uiView.SetVisibility(true);
        uiView.CustomStartAnchoredPosition = location.anchoredPosition;
        uiView.ResetPosition();
        location.gameObject.SetActive(false);
    }
}