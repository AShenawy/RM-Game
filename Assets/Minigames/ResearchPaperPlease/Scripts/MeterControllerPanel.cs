using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MeterControllerPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel;

    private bool isMouseOverButton = false;
    private bool isMouseOverPanel = false;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOverButton = true;
        ShowPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOverButton = false;
        StartCoroutine(CheckMouseExit());
    }

    // Panel's own handler for mouse enter/exit
    public void OnPanelPointerEnter(PointerEventData eventData)
    {
        isMouseOverPanel = true;
    }

    public void OnPanelPointerExit(PointerEventData eventData)
    {
        isMouseOverPanel = false;
        StartCoroutine(CheckMouseExit());
    }

    private IEnumerator CheckMouseExit()
    {
        yield return new WaitForSeconds(0.1f);

        if (!isMouseOverButton && !isMouseOverPanel)
        {
            HidePanel();
        }
    }

    void ShowPanel()
    {
        infoPanel.SetActive(true);
    }

    void HidePanel()
    {
        infoPanel.SetActive(false);
    }
}

