using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    public class RoomData : MonoBehaviour
    {
        [Tooltip("Sets whether player can turn around in the room or face one direction only. Default: True.")]
        public bool playerCanTurn = true;
        
        [Tooltip("If player can turn around, sets how many degrees a single turn does. Should be 0, 90, or 180")]
        public int allowedTurnAngle;

        [Tooltip("Force player to face a direction when entering the room")]
        public bool ForcePlayerOrientation = false;
        
        public enum Orientations { North, East, South, West }
        [Tooltip("Direction the player will be facing when entering the room")]
        public Orientations orientation;
        
        [Tooltip("Does this room count as a checkpoint to save progress at? Default: False.")]
        public bool isSavePoint = false;
    }
}