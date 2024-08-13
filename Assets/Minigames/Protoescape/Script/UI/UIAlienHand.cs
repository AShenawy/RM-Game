using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlienHand : MonoBehaviour
    {
        [SerializeField] private GameObject alienHand;
        [SerializeField] private RectTransform hintPanel;
        [SerializeField] private TMPro.TextMeshProUGUI hintText;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip likeSound;
        [SerializeField] private AudioClip confuseSound;

        private readonly Vector2 _messagePanelPaddingSize = new Vector2(10, 10);

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

            hintPanel.gameObject.SetActive(true);
            hintText.text = "";

            var confusedCategories = checkable.GetConfusedCategories();
            var likedCategories = checkable.GetLikedCategories();

            foreach (var liked in likedCategories)
                hintText.text += $"<b><sprite=\"checkmark\" index=0>{liked}\n</b>";

            foreach (var confused in confusedCategories)
                hintText.text += $"<b><sprite=\"cross\" index=0>{confused}\n</b>";

            if (confusedCategories.Count == 0)
            {
                audioSource.clip = likeSound;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = confuseSound;
                audioSource.Play();
            }

            hintText.ForceMeshUpdate();

            Vector2 textSize = hintText.GetRenderedValues();
            hintPanel.sizeDelta = new Vector2(textSize.x, textSize.y) + _messagePanelPaddingSize;
        }

        private void OnDestroy()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
        }
    }
}