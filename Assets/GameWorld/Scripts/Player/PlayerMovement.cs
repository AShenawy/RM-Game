using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool canTurn = true;
        public int turnAngle = 90;
        public bool canDimeSwitch = true;
    
        // Removed ability to turn with keyboard. Player can only turn using on-screen interface
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        //        RotateClockwise();

        //    if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        //        RotateCounterclockwise();
        //}
    
        public void RotateClockwise()
        {
            transform.Rotate(0, turnAngle, 0);
        }

        public void RotateCounterclockwise()
        {
            transform.Rotate(0, -turnAngle, 0);
        }

        // Needs to be done through game manager
        //public void DoSwitch()
        //{
        //    if (canDimeSwitch)
        //    {
        //        float newHlPosition = transform.position.x * -1;
        //        transform.position = new Vector3(newHlPosition, transform.position.y, transform.position.z);
        //    }
        //    else
        //    {
        //        DialogueHandler.instance.DisplayDialogue("I can't switch dimensions right now.");
        //    }
        //}
    }
}