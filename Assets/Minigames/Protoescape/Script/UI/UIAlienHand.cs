using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlienHand : MonoBehaviour
    {
        [SerializeField] private GameObject alienHand;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip likeSound;
        [SerializeField] private AudioClip confuseSound;

        private void Awake()
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
                audioSource.clip = likeSound;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = confuseSound;
                audioSource.Play();
            }
        }

        private void OnDestroy()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
        }
    }
}