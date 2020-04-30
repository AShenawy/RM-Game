using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetRoom;

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    BoxCollider2D doorCollider = GetComponent<BoxCollider2D>();

        //    if(doorCollider.OverlapPoint(mouseLocation))
        //    {
        //        GoToRoom(targetRoom);
        //    }
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.gm.GoToRoom(targetRoom);
    }
}
   
