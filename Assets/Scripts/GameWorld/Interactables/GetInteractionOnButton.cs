using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInteractionOnButton : MonoBehaviour
{
    public Interaction interactionType;

    public void DoInteraction()
    {
        GameManager.instance.InteractWithObject(interactionType);
    }
}