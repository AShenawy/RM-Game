using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITutorPrefab : MonoBehaviour, IPointerUpHandler
{
    public Slider slider;
    public GameObject objectToDeactivate;
    public GameObject tutorial;

    void Start()
    {
        EventTrigger trigger = slider.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = slider.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });

        trigger.triggers.Add(pointerUpEntry);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (slider.value >= slider.maxValue)
        {
            objectToDeactivate.SetActive(false);
        }
    }

    public void DestroyTutorial()
    {
        Destroy(tutorial);
    }
}
