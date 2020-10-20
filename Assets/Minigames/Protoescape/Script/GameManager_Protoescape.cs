using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        public static event Action<bool> OnStackMove = delegate { };
        public static event Action<GameObject> OnSelected = delegate { };

        public static GameObject SelectedEntity { get => _selectedEntity; set { _selectedEntity = value; OnSelected?.Invoke(value); } }
        private static GameObject _selectedEntity;

        public static bool IsStacksMovable { get => _isStacksMovable; set { _isStacksMovable = value; OnStackMove?.Invoke(value); } }
        static bool _isStacksMovable;

        private List<IEntity> _likableEntities;
        private List<IEntity> _confusingEntities;

        public void CheckResult()
        {
            _confusingEntities = _likableEntities = new List<IEntity>();

            var entities = GetComponentsInChildren<IEntity>();

            foreach (var entity in entities)
            {
                if (entity.CorrectSiblingIndex == entity.CurrentSiblingIndex)
                {
                    _likableEntities.Add(entity);
                }
                else
                {
                    _confusingEntities.Add(entity);
                }
            }
        }

        private void Start()
        {
            IsStacksMovable = false;
        }
    }
}