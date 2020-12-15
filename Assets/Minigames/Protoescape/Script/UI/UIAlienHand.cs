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

            var result = checkable.GetConfusedCategories();

            if (result.Count == 0)
            {
                hintPanel.gameObject.SetActive(false);
                audioSource.clip = likeSound;
                audioSource.Play();
            }
            else
            {
                hintPanel.gameObject.SetActive(true);
                audioSource.clip = confuseSound;
                audioSource.Play();

                hintText.text = "Confused:";

                foreach (var confused in result)
                {
                    hintText.text += $" <b>[{confused}]</b>";
                }
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