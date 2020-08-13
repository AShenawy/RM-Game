using UnityEngine;

namespace Methodyca.Core
{
    // This script is for digit scrolling button. Since the OnClick() event built in buttons doesn't accept
    // enums, this script will make the button activate DoInteraction() method while the enum value
    // is set in scrollDirection field.
    public class SetScrollDirection : MonoBehaviour
    {
        [Tooltip("Game Object which has the Scroll Digits script on it")]
        public ScrollDigits scrollDigitObject;
        [Tooltip("Which direction do the digits scroll")]
        public ScrollDirection scrollDirection;

        public void DoInteraction()
        {
            scrollDigitObject.Scroll(scrollDirection);
        }
    }
}