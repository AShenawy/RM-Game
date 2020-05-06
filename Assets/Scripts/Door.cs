using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetRoom;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.gm.GoToRoom(targetRoom);
    }
}
   
