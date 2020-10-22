using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuggestionManager : MonoBehaviour
{
    public GameObject[] speechBubbles;
    public GameObject acceptButton;
    public GameObject passButton;
    public DesignPlan designPlan;
    public Idea presentedIdea;
    
    private CanvasGroup attendantsCanvasGroup;

    private void OnEnable()
    {
        attendantsCanvasGroup = GetComponent<CanvasGroup>();
        attendantsCanvasGroup.blocksRaycasts = true;
        HideSpeechBubbles();
        DisplayChoices(false);
    }

    public void OnSuggestionClicked(GameObject bubble, Idea idea)
    {
        attendantsCanvasGroup.blocksRaycasts = false;
        presentedIdea = idea;

        DisplaySpeechBubble(bubble);
        DisplayChoices(true);

    }

    public void OnSuggestionEnded()
    {
        attendantsCanvasGroup.blocksRaycasts = true;

        HideSpeechBubbles();
        DisplayChoices(false);
    }

    public void AcceptPresentedIdea()
    {
        designPlan.SaveIdea(presentedIdea);
    }

    void DisplaySpeechBubble(GameObject bubble)
    {
        for (int i = 0; i < speechBubbles.Length; i++)
        {
            if (speechBubbles[i] == bubble)
            {
                speechBubbles[i].SetActive(true);

                TMP_Text bubbleText = speechBubbles[i].GetComponentInChildren<TMP_Text>();
                bubbleText.text = presentedIdea.idea;
            }
        }
    }

    void HideSpeechBubbles()
    {
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }
    }

    void DisplayChoices(bool value)
    {
        acceptButton.SetActive(value);
        passButton.SetActive(value);
    }
}
