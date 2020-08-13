using Methodyca.Core;
using UnityEngine;


// This script handles lockbox and safe objects that need a code to unlock/open
public class LockBox : ObjectInteraction
{
    [Header("Specific Lock Box Parameters")]
    [SerializeField] private GameObject lockedBoxPrefab;
    [SerializeField] private GameObject unlockedBoxPrefab;
    [SerializeField, Tooltip("Is box already locked?")] private bool isLocked = true;

    public override void InteractWithObject()
    {
        if (isLocked)
        {
            //TODO Display the safe closeup
        }
        else
            Open();
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void Open()
    {
        canInteract = false;
        DisableInteractionCollider();
        ReplaceWithOpenBox(unlockedBoxPrefab);
    }

    void ReplaceWithOpenBox(GameObject boxPrefab)
    {
        Destroy(lockedBoxPrefab);  // remove locked box child
        Instantiate(unlockedBoxPrefab, gameObject.transform);   // instaniate unlocked box version as child
    }
}
