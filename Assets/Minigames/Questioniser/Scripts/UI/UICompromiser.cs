using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class UICompromiser : MonoBehaviour
    {
        [SerializeField] Button fiveForTwo;
        [SerializeField] Button tenForFive;

        void OnEnable()
        {
            fiveForTwo.onClick.AddListener(FiveForTwoClickHandler);
            tenForFive.onClick.AddListener(TenForFiveClickHandler);
        }

        void TenForFiveClickHandler()
        {
            GameManager.Instance.HandleCompromiser(-10, 5);
            gameObject.SetActive(false);
        }

        void FiveForTwoClickHandler()
        {
            GameManager.Instance.HandleCompromiser(-5, 2);
            gameObject.SetActive(false);
        }

        void OnDisable()
        {
            fiveForTwo.onClick.RemoveAllListeners();
            tenForFive.onClick.RemoveAllListeners();
        }
    }
}