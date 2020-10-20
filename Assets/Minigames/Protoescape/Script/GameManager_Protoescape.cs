using System;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        public static event Action<bool> OnStackMove = delegate { };
        public static event Action<GameObject> OnSelected = delegate { };
        public static bool IsStacksMovable = false;

        public static GameObject SelectedEntity { get => _selectedEntity; set { { _selectedEntity = value; OnSelected?.Invoke(_selectedEntity); } } }
        private static GameObject _selectedEntity;

        public void HandleStacksMove(bool value)
        {
            IsStacksMovable = value;
            OnStackMove?.Invoke(value);
        }
    }
}