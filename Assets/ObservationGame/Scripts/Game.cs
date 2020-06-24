using Doozy.Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;

    [Range(0, 10)] public int ObservationDays;
    public UIView TaskView;
    public UIView ServiceView;
    public TextMeshProUGUI ReportText;
    public Transform Services;
    public Transform Counters;

    public Dictionary<VisitorType, byte> Visitors { get; private set; }

    byte day = 1;
    public const byte DESIRED_SIZE = 5;
    Counter[] allCounters;
    Service[] allServices;
    Dictionary<VisitorType, Service> availableServices;

    public event Action<byte> OnRecorded = delegate { };
    private void Awake()
    {
        if (instance == null)
            instance = this;

        Visitors = new Dictionary<VisitorType, byte>();
        availableServices = new Dictionary<VisitorType, Service>();
        allServices = Services.GetComponentsInChildren<Service>();
        allCounters = Counters.GetComponentsInChildren<Counter>();

        foreach (var service in allServices)
        {
            if (!availableServices.ContainsKey(service.Type))
                availableServices.Add(service.Type, service);
        }
    }

    void OnEnable() => Service.OnLocated += Service_OnLocated;
    void Service_OnLocated(Service service) => availableServices[service.Type].IsLocated = true;

    public void Record()
    {
        day++;
        if (day > ObservationDays)
        {
            TaskView.InstantHide();
            ServiceView.SetVisibility(true);
            return;
        }

        for (int i = 0; i < allCounters.Length; i++)
        {
            if (Visitors.ContainsKey(allCounters[i].Type))
                Visitors[allCounters[i].Type] += allCounters[i].Count;
            else
                Visitors.Add(allCounters[i].Type, allCounters[i].Count);
        }

        OnRecorded?.Invoke(day);
    }

    public void Report()
    {
        foreach (var key in Visitors.Where(x => x.Value >= DESIRED_SIZE).Select(x => x.Key).ToList())
        {
            if (availableServices[key].IsLocated)
            {
                //Population ok, service ok
                ReportText.text += string.Format($"\n- Locating the <b>{availableServices[key].Name}</b> service was a great decision since there are enough visitors.\n");
            }
            else
            {
                //Population ok, service not ok
                ReportText.text += string.Format($"\n- You recorded many visitors for <b>{availableServices[key].Name}</b> service, but it's missing somehow.\n");
            }
        }
        foreach (var key in Visitors.Where(x => x.Value < DESIRED_SIZE).Select(x => x.Key).ToList())
        {
            if (availableServices[key].IsLocated)
            {
                //Population not ok, service ok
                ReportText.text += string.Format($"\n- Not much visitors to use <b>{availableServices[key].Name}</b> service but you decided to locate anyway. Was it a future investment?\n");
            }
            else
            {
                //Population not ok, service not ok
                ReportText.text += string.Format($"\n- If there isn't visitors then there isn't <b>{availableServices[key].Name}</b> service.\n");
            }
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}