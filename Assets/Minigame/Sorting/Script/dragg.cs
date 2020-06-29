using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragg : MonoBehaviour, IPointerDownHandler
, IBeginDragHandler, IEndDragHandler, IDragHandler
   {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("StartDrag");
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("WhileDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("EndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("ClickedDragged");

    }

}
