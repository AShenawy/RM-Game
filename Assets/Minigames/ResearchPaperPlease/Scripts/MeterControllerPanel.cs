using UnityEngine;
using UnityEngine.UI;

public class MeterControllerPanel : MonoBehaviour
{
    public GameObject infoPanel;   // The panel to display
    public Button leftMeterButton; // The meter (which should be clickable)

    void Start()
    {
        // Ensure the panel is hidden initially
        infoPanel.SetActive(false);

        // Add a listener to the meter button to show the panel
        leftMeterButton.onClick.AddListener(ShowPanel);

        // Add a listener to the panel's button component to hide the panel
        infoPanel.GetComponent<Button>().onClick.AddListener(HidePanel);
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

