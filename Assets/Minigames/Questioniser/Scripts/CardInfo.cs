using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] CardBase card;

        CardInfoUI _infoGUI;
        CardData _data;

        void Start()
        {
            _infoGUI = GameManager.Instance.CardInfoGUI;
            _data = card.GetData;
        }

        void OnMouseEnter()
        {
            _infoGUI.gameObject.SetActive(true);
            _infoGUI.SetData(_data.Description);
        }

        void OnMouseExit()
        {
            _infoGUI.gameObject.SetActive(false);
        }
    }
}