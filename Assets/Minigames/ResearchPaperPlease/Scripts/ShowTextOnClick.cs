using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnClick : MonoBehaviour
{
    public GameObject panelObject; // Reference to the Panel GameObject (the background)

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the panel (and text inside it) is initially invisible
        panelObject.SetActive(false);

        // Add a listener to the panel or text to detect clicks (assuming text is a child of panel)
        if (panelObject.TryGetComponent(out Button panelButton))
        {
            panelButton.onClick.AddListener(HidePanel);
        }
        else
        {
            Debug.LogWarning("The panelObject does not have a Button component. Please add a Button component to make it clickable.");
        }
    }

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {
        panelObject.SetActive(true); // Show the panel (and the text with it)
    }

    // This method will be called when the panel or text is clicked
    public void HidePanel()
    {
        panelObject.SetActive(false); // Hide the panel (and the text with it)
    }
}



