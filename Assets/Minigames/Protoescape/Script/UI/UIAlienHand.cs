using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlienHand : MonoBehaviour
    {
        [SerializeField] private RectTransform alienHand;

        private void OnEnable()
        {
            PrototypeTester.OnSelectionPointed += SelectionPointedHandler;
        }

        private void SelectionPointedHandler(CategoryType confusion, GameObject selection)
        {
            alienHand.anchoredPosition = selection.GetComponent<RectTransform>().anchoredPosition;

            if (confusion == CategoryType.None)
            {
                //Likable
            }
            else
            {
                //Confusing
            }
        }

        private void OnDisable()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
        }
    }
}