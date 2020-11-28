using UnityEngine;
using Methodyca.Core;

// This script handles general use of objects
[RequireComponent(typeof(SwitchImageDisplay))]
public class Operate : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Operate Parameters")]
    [Tooltip("Can the player operate this object?")]
    public bool canOperate;
    [Tooltip("Dialogue to display on successful operate")]
    public string onOperateSuccessText;
    [Tooltip("Dialogue to display on failed operate")]
    public string onOperateFailText;

    private bool isOperated;


    protected override void Start()
    {
        LoadState();
        if (isOperated)
            Use();

        base.Start();
    }

    public override void InteractWithObject()
    {
        if (canOperate)
        {
            Use();
            DialogueHandler.instance.DisplayDialogue(onOperateSuccessText);
        }
        else
            DialogueHandler.instance.DisplayDialogue(onOperateFailText);
    }

    void Use()
    {
        GetComponent<SwitchImageDisplay>().SwitchImage();
        isOperated = true;
        ToggleInteraction(false);
        SaveState();
    }

    public void SaveState()
    {
        SaveLoadManager.SetInteractableState(name, isOperated ? 1 : 0);
    }

    public void LoadState()
    {
        if (SaveLoadManager.interactableStates.TryGetValue(name, out int operatedState))
            isOperated = (operatedState == 0) ? false : true;
    }
}
   
