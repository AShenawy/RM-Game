using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    public class AddMail : MonoBehaviour
    {
        public GameObject mailingList;


        public void Add(GameObject mailPrefab)
        {
            GameObject instance = Instantiate(mailPrefab, mailingList.transform);

            instance.transform.SetAsFirstSibling();
        }
    }
}