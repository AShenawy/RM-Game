using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
  public class TextInput : MonoBehaviour
  {
      public Text inputText;
      public bool isTyping = false;

      // Update is called once per frame
      void Update()
      {
          if (Input.GetKeyDown(KeyCode.Return))
          {
              isTyping = !isTyping;
          }

          if (isTyping)
          {
              foreach(char c in Input.inputString)
              {
                  if (c == '\b' && inputText.text.Length > 0)
                  {
                      inputText.text = inputText.text.Remove(inputText.text.Length - 1);
                  }
                  else
                  {
                      inputText.text += c;
                  }
              }
          }
      }
  }
}