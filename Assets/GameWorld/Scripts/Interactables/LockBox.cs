using Methodyca.Core;
using UnityEngine;
using UnityEngine.UI;


// This script handles lockbox and safe objects that need a code to unlock/open
public class LockBox : ObjectInteraction
{
    [Header("Specific Lock Box Parameters")]
    [SerializeField, Tooltip("Is box already locked?")]
    private bool isLocked = true;
    
    [SerializeField, Tooltip("Correct combination to unlock box")]
    private int[] unlockCombination;
    
    [SerializeField] private GameObject lockedBoxPrefab;
    
    [SerializeField, Tooltip("UI Game Object to display lock interface")]
    private GameObject lockScreen;

    [Tooltip("Game objects controlling values on the safe. Must have Scroll Digits script")]
    public ScrollDial[] lockDials;
    
    [SerializeField, Tooltip("Light indicator on lock interface")] 
    private Image lockIndicator;

    [SerializeField] private GameObject unlockedBoxPrefab;
    
    [SerializeField, Tooltip("Message to display when unlocked")]
    private string winMessage;

    public Sound SFX;

    private void Start()
    {
        // subscribe to event called by changing values on lock dials
        foreach (ScrollDial dial in lockDials)
        {
            dial.onDigitChanged += CheckCombination;
        }
    }

    public override void InteractWithObject()
    {
        if (isLocked)
            DisplayLockScreen();
        else
            Open();
        
    }

    void DisplayLockScreen()
    {
        lockScreen.SetActive(true);
    }

    void CheckCombination()
    {
        for (int i = 0; i < lockDials.Length; i++)
        {
            // stop the check if one of of the numbers on the dials is wrong
            if (lockDials[i].currentValue != unlockCombination[i])
                break;
            else
            {
                // if reached last combination digit and is correct then unlock. Otherwise continue to next iteration.
                if (i >= lockDials.Length - 1)
                {
                    DialogueHandler.instance.DisplayDialogue(winMessage);
                    Unlock();
                }

                continue;
            }
        }
    }

    void Unlock()
    {
        isLocked = false;

        // change indicator colour if available
        if (lockIndicator)
            lockIndicator.color = Color.green;
            Debug.Log("Setlockmatch");
            SoundManager.instance.PlaySFX(SFX);
    }

    void Open()
    {
        Destroy(lockedBoxPrefab);  // remove locked box child
        Instantiate(unlockedBoxPrefab, gameObject.transform);   // instaniate unlocked box version as child
        ToggleInteraction(false);
    }
}
