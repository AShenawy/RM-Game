using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    public class GameStarter : MonoBehaviour
    {
        public GameObject gameStartRoom;

        public void GoToGameStart()
        {
            GameManager.instance.GoToRoom(gameStartRoom);
        }
    }
}