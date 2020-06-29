using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dragslot : MonoBehaviour, IDropHandler
{

    int points;
    GameObject battery;

    public void OnDrop(PointerEventData eventData)
    {
        if (battery == null)
        {
            battery = GameObject.Find("dockstation");
        }

        if (eventData.pointerDrag == null)
        {
            return;
        }

        Debug.Log("Dropped");
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        
        if (points < 5)
        {
            points++;
            Sprite sprite = Resources.Load<Sprite>("ChargingRates/charger_" + points.ToString());
            battery.GetComponent<Image>().overrideSprite = sprite;
        }
        
       
    }


}
