using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // This script handles the interface buttons for lock box/safe objects
    public class ScrollDial : MonoBehaviour
    {
        [Tooltip("Display game object")]
        public Text digitDisplay;
        [Tooltip("Values to be displayed")]
        public int[] digits;
        public delegate void OnDigitChanged();
        public event OnDigitChanged onDigitChanged;

        private int currentIndex = 0;
        public int currentValue { get;  private set; }

        private void Start()
        {
            UpdateDisplay(currentIndex);
        }

        private void UpdateDisplay(int index)
        {
            currentValue = digits[index];
            digitDisplay.text = currentValue.ToString();
            onDigitChanged?.Invoke();
        }

        public void Scroll(ScrollDirection direction)
        {
            switch (direction)
            {
                case ScrollDirection.Down:
                    // check index doesn't go out of digits array bounds
                    if (currentIndex < digits.Length - 1)
                        currentIndex++;
                    else
                        currentIndex = 0;

                    UpdateDisplay(currentIndex);
                    break;

                case ScrollDirection.Up:
                    if (currentIndex > 0)
                        currentIndex--;
                    else
                        currentIndex = digits.Length - 1;

                    UpdateDisplay(currentIndex);
                    break;
            }
        }
    }

    public enum ScrollDirection { Up, Down }
}