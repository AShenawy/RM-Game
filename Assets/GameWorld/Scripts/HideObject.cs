using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
  public class HideObject : MonoBehaviour
  {
      public void Hide(GameObject gameObject)
      {
          gameObject.SetActive(false);
      }

  }
}