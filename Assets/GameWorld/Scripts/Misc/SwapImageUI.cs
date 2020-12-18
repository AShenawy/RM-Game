using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // this script switches UI button graphic
    public class SwapImageUI : MonoBehaviour
    {
        public Sprite imageA;
        public Sprite imageB;

        public void SwapImage()
        {
            // get the currently displayed image in UI
            Sprite displayedImage = GetComponent<Image>().sprite;

            if (displayedImage == imageA)
                GetComponent<Image>().sprite = imageB;
            else if (displayedImage == imageB)
                GetComponent<Image>().sprite = imageA;
            else
                return;
        }
    }
}