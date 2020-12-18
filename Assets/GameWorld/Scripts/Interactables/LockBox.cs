using Methodyca.Core;
using UnityEngine;
using UnityEngine.UI;


// This script handles lockbox and safe objects that need a code to unlock/open
public class LockBox : ObjectInteraction, ISaveable, ILoadable
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
    public Sound dialSFX;
    public Sound unlockedSFX;

    private bool isOpened;


    protected override void Start()
    {
        // check for saved state
        LoadState();
        if (!isLocked)
            Unlock();
        if (isOpened)
            Open();

        // subscribe to event called by changing values on lock dials
        foreach (ScrollDial dial in lockDials)
        {
            dial.onDigitChanged += CheckCombination;
            dial.onDigitChanged += PlayDialSFX;
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

    void PlayDialSFX()
    {
        SoundManager.instance.PlaySFX(dialSFX);
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
                    if (isLocked)
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

        SoundManager.instance.PlaySFXOneShot(unlockedSFX);
        SaveState();
    }

    void Open()
    {
        Destroy(lockedBoxPrefab);  // remove locked box child
        Instantiate(unlockedBoxPrefab, gameObject.transform);   // instaniate unlocked box version as child
        ToggleInteraction(false);
        isOpened = true;
        SaveState();
    }

    public void SaveState()
    {
        // make 2 entries as same key can't be used twice
        SaveLoadManager.SetInteractableState(name + "_locked", System.Convert.ToInt32(isLocked));
        SaveLoadManager.SetInteractableState(name + "_opened", System.Convert.ToInt32(isOpened));
    }

    public void LoadState()
    {
        if (SaveLoadManager.interactableStates.TryGetValue(name + "_locked", out int lockedState))
            isLocked = (lockedState == 0) ? false : true;

        if (SaveLoadManager.interactableStates.TryGetValue(name + "_opened", out int openedState))
            isOpened = (openedState == 0) ? false : true;
    }
}
