using UnityEngine;

// This script translate a game object in specified direction and speed
namespace Methodyca.Core
{
    public class Translate : MonoBehaviour
    {
        private enum Direction { Right, Left, Up, Down};
        [SerializeField] private Direction translationDirection;
        public float translationSpeed = 5f;

        private void Update()
        {
            switch (translationDirection)
            {
                case (Direction.Right):
                    DoTranslation(Vector3.right);
                    break;

                case (Direction.Left):
                    DoTranslation(Vector3.left);
                    break;

                case (Direction.Up):
                    DoTranslation(Vector3.up);
                    break;

                case (Direction.Down):
                    DoTranslation(Vector3.down);
                    break;
            }
        }

        private void DoTranslation(Vector3 direction)
        {
            transform.Translate(direction * translationSpeed * Time.deltaTime);
        }
    }
}