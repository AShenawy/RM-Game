using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class UIProfileImageHandler : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Sprite male;
        [SerializeField] Sprite female;

        private void Start()
        {
            if (GenderManager.Current.IsMale)
                image.sprite = male;
            else
                image.sprite = female;
        }
    }
}