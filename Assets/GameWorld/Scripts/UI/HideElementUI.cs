using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    // This generic script is used to hide objects on button click
    public class HideElementUI : MonoBehaviour
    {
        public void HideObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}