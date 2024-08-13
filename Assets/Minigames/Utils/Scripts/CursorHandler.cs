using System;
using UnityEngine;

namespace Methodyca.Minigames.Utils
{
    public class CursorHandler : MonoBehaviour
    {
        public static event Action OnCursorChange = delegate { };

        [SerializeField] Texture2D cursor;

        public static void SetNewCursor(Texture2D cursor)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            OnCursorChange?.Invoke();
        }

        private void Start()
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            OnCursorChange?.Invoke();
        }
    }
}