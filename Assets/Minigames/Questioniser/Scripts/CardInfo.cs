using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] CardBase card;

        CardInfoUI _infoGUI;

        void Start()
        {
            _infoGUI = GameManager.Instance.CardInfoGUI;

        }

        void OnMouseEnter()
        {
            _infoGUI.gameObject.SetActive(true);
            _infoGUI.SetData(card.Description);
        }

        void OnMouseExit()
        {
            _infoGUI.gameObject.SetActive(false);
        }
    }
}