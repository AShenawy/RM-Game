using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
  public class RoomData : MonoBehaviour
  {
      [Tooltip("Sets whether player can turn around in the room or face one direction only")]
      public bool playerCanTurn = true;
      [Tooltip("If player can turn around, sets how many degrees a single turn does. Should be 0, 90, or 180")]
      public int allowedTurnAngle;
  }
}