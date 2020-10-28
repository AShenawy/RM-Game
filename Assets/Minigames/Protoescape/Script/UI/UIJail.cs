using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIJail : MonoBehaviour
    {
        [SerializeField] private RectTransform alien;
        [SerializeField] private TMPro.TextMeshProUGUI testText;
        [SerializeField] private Button prototypeButton;

        private void OnEnable()
        {
            prototypeButton.onClick.AddListener(ClickPrototypeHandler);
            PrototypeTester.OnPrototypeTested += PrototypeTestedHandler;
        }

        private void PrototypeTestedHandler(int current, int total)
        {
            testText.text = $"{current}/{total}";
        }

        private void ClickPrototypeHandler()
        {
            GameManager_Protoescape.Instance.HandlePrototypeInitiation();
        }

        private void OnDisable()
        {
            prototypeButton.onClick.RemoveAllListeners();
            PrototypeTester.OnPrototypeTested -= PrototypeTestedHandler;
        }
    }
}