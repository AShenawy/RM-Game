using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    // This generic script is used to destroy objects on button click
    public class DestroyElementUI : MonoBehaviour
    {
        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}