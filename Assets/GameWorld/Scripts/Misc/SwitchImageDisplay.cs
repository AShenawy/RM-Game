using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // This script allows to hide/display/switch 2 images on game objects' sprite renderers
    public class SwitchImageDisplay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite firstImage, secondImage;

        public void ShowImage()
        {
            spriteRenderer.enabled = true;
        }

        public void HideImage()
        {
            spriteRenderer.enabled = false;
        }

        public void SwitchImage()
        {
            // check there are actually images set
            if (firstImage == null || secondImage == null)
                return;

            Sprite tempImage = firstImage;
            spriteRenderer.sprite = secondImage;
            
            // switch the image order to switch images each time method is called
            firstImage = secondImage;
            secondImage = tempImage;
        }

        public void ChangeImage(Sprite newImage)
        {
            // check that an image is actually provided
            if (newImage == null)
                return;

            secondImage = spriteRenderer.sprite;
            firstImage = newImage;
            spriteRenderer.sprite = firstImage;
        }
    }
}