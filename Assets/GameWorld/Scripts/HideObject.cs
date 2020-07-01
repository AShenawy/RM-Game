using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorld
{
    public class HideObject : MonoBehaviour
    {
        public void Hide(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

    }
}