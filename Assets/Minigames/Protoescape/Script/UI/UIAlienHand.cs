using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlienHand : MonoBehaviour
    {
        [SerializeField] private GameObject alienHand;
        [SerializeField] private AudioClip likeSound;
        [SerializeField] private AudioClip confuseSound;

        private void Start()
        {
            PrototypeTester.OnSelectionPointed += SelectionPointedHandler;
        }

        private void SelectionPointedHandler(ICheckable checkable)
        {
            if (checkable == null) // There is no item to point anymore
            {
                alienHand.SetActive(false);
                return;
            }

            alienHand.transform.position = checkable.gameObject.GetComponent<RectTransform>().position;
            var likables = checkable.GetLikables();

            if (likables.Count > 0)
            {
                //likable sound

            }
            else
            {
                //confusing sound

            }
        }

        private void OnDestroy()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
        }
    }
}