using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Methodyca.Core
{
    public class Mail : MonoBehaviour
    {
        public TMP_Text sender;
        public TMP_Text subject;
        public TMP_Text body;
        public Image icon;

        public bool isRead = false;

        public void Display()
        {
            isRead = true;
            GetComponent<Image>().color =  Color.white;
            MailManager.instance.DisplayMail(this);
        }
    }
}