using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    public class GameManager : MonoBehaviour
    {
        public bool filesArranged = false;
        public GameObject filesSorted;
        public void Complete()
        {
            filesSorted.SetActive(true);
            Debug.Log("You Sabi");
        }
    }

}
