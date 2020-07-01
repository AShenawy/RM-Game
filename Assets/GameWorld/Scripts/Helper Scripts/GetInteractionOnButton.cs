using UnityEngine;

namespace GameWorld
{
    // This script is for context menu buttons. Since the OnClick() event built in buttons doesn't accept
    // enums, this script will make the button activate DoInteraction() method while the enum value
    // is set in interactionType field.
    public class GetInteractionOnButton : MonoBehaviour
    {
        public Interaction interactionType;

        public void DoInteraction()
        {
            GameManager.instance.InteractWithObject(interactionType);
        }
    }
}