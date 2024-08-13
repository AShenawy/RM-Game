//#define TESTING
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

        #region Test Code Vars
        // typing in this string during gameplay sends the messages app a test mail
        private string[] sndml = new string[] {"s", "n", "d", "m", "l"};
        private int sndmlIndex = 0;
        #endregion

        private void Update()
        {
            // ========== TEST CODE INPUT ==========
#if TESTING
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(sndml[sndmlIndex]))
                    sndmlIndex++;
                else
                    sndmlIndex = 0;
            }

            if (sndmlIndex == sndml.Length)
            {
                SendMail(mailSample);
                sndmlIndex = 0;
            }
#endif
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

            // Reset the counter to not add up the numbers in below foreach statement
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
                DisplayNotificationIcons(false);
            }
            else if(unreadMailCount > 9)
            {
                counter.text = "9+";
                DisplayNotificationIcons(true);
            }
            else
            {
                counter.text = unreadMailCount.ToString();
                DisplayNotificationIcons(true);
            }
        }

        void DisplayNotificationIcons(bool value)
        {
            mailAppNotifier.SetActive(value);   // hide notification icon on App
            inventoryIconNotifier.enabled = value;  // hide notification icon on phone inventory icon
        }
    }
}