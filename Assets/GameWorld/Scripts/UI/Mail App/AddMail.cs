using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // ******** This script was for debugging, is no longer needed and should be deleted *********

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