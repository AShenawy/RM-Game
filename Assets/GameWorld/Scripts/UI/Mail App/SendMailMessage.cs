using UnityEngine;

namespace Methodyca.Core
{
    // This script allows the game object it's on to send a message/tip to mail phone app
    public class SendMailMessage : MonoBehaviour
    {
        public GameObject mailPrefab;

        public void Send()
        {
            MailManager.instance.SendMail(mailPrefab);
        }
    }
}