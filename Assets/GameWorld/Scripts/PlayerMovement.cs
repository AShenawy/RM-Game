using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
  public class PlayerMovement : MonoBehaviour
  {
      public string buttonRight;
      public string buttonLeft;
      public bool canTurn = true;
      public int turnAngle = 90;

      void Update()
      {
          if (Input.GetKeyDown(buttonRight))
              Turn("right");

          else if (Input.GetKeyDown(buttonLeft))
              Turn("left");
      }

      public void Turn(string direction)
      {
          if (!canTurn) return;

          switch (direction)
          {
              case "right":
                  transform.Rotate(0, turnAngle, 0);
                  break;
              case "left":
                  transform.Rotate(0, -turnAngle, 0);
                  break;
          }
      }
  }
}