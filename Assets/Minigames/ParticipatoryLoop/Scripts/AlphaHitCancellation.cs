using UnityEngine;
using UnityEngine.UI;


namespace Methodyca.Minigames
{
    public class AlphaHitCancellation : MonoBehaviour
    {
        Image image;

        // Start is called before the first frame update
        void Start()
        {
            image = GetComponent<Image>();
            image.alphaHitTestMinimumThreshold = 0.3f;
        }
    }
}