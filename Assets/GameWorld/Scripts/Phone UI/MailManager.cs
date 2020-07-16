using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace Methodyca.Core
{
    // This script handles the mail receieved into player phone
    public class MailManager : MonoBehaviour
    {
        #region Singleton
        public static MailManager instance;
        private void Awake()
        {
            if(instance == null)
                instance = this;
        }
        #endregion

        [Header("Indicators")]
        public GameObject mailAppNotifier;
        public Image inventoryIconNotifier;

        [Header("Mail List")]
        public GameObject mailList;
        public GameObject listContent;
        public GameObject mailSample;

        [Header("Mail Display")]
        public GameObject mailDisplay;
        public TMP_Text mailSubject;
        public TMP_Text mailSender;
        public Image senderIcon;
        public TMP_Text mailBody;

        private int unreadMailCount = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                SendMail(mailSample);
        }

        public void DisplayMail(Mail mail)
        {
            mailSubject.text = mail.subject.text;
            mailSender.text = mail.sender.text;
            senderIcon.sprite = mail.icon.sprite;
            senderIcon.color = mail.icon.color;
            mailBody.text = mail.body.text;
            
            // Hide mail list and show filled details in mail display
            mailList.SetActive(false);
            mailDisplay.SetActive(true);
            
            UpdateNotificationCounter();
        }

        public void HideMail()
        {
            mailDisplay.SetActive(false);
            mailList.SetActive(true);

            mailSubject.text = null;
            mailSender.text = null;
            senderIcon.sprite = null;
            senderIcon.color = Color.white;
            mailBody.text = null;
        }

        public void SendMail(GameObject mailPrefab)
        {
            // add new mail to mail list
            GameObject newMail = Instantiate(mailPrefab, listContent.transform);
            newMail.transform.SetAsFirstSibling();

            UpdateNotificationCounter();
        }

        void UpdateNotificationCounter()
        {
            // Update unread mail counter
            Mail[] mailReceived = listContent.GetComponentsInChildren<Mail>(true);

            // Reset the count to not add up the numbers in below foreach statement
            unreadMailCount = 0;

            foreach (Mail mail in mailReceived)
            {
                if (!mail.isRead)
                    unreadMailCount++;
            }

            Text counter = mailAppNotifier.GetComponentInChildren<Text>();

            // check what to display in the message notification
            if(unreadMailCount < 1)
            {
                counter.text = "";
                mailAppNotifier.SetActive(false);  // hide notification icon on App
                inventoryIconNotifier.enabled = false; // hide notification icon on phone inventory icon
            }
            else if(unreadMailCount > 9)
            {
                counter.text = "9+";
                mailAppNotifier.SetActive(true);
                inventoryIconNotifier.enabled = true;
            }
            else
            {
                counter.text = unreadMailCount.ToString();
                mailAppNotifier.SetActive(true);
                inventoryIconNotifier.enabled = true;
            }
        }
    }
}