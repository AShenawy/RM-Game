using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
  public class ShowObject : MonoBehaviour
  {
      public void Show(GameObject gameObject)
      {
          gameObject.SetActive(true);
      }
  }
}