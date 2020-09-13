using UnityEngine;

// This scripts teleports a game object to the opposite side of the screen once it exits it (for 2D assets)
namespace Methodyca.Core
{
    [RequireComponent(typeof(SpriteRenderer))]  // this script currently works only with SpriteRender component
    public class WrapAround2D : MonoBehaviour
    {
        private enum ExitDirection { Right, Left, Top, Bottom };
        [SerializeField] private ExitDirection objectExitSide;      // which side of the screen will object exit it?
        public int edgeBuffer = 10;     // how many pixels after complete exit before object is wrapped around?

        SpriteRenderer spriteRend;

        // Start is called before the first frame update
        void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckBoundaryConditions();
        }

        void CheckBoundaryConditions()
        {
            // get min and max edges of sprite in screen space to compare against play screen
            Vector3 minSpriteBounds = Camera.main.WorldToScreenPoint(spriteRend.bounds.min);
            Vector3 maxSpriteBounds = Camera.main.WorldToScreenPoint(spriteRend.bounds.max);

            // if any of the edges exited the screen, then wrap the object around
            switch (objectExitSide)
            {
                case ExitDirection.Right:       // check the left-most point of sprite
                    if ((int)minSpriteBounds.x - edgeBuffer > Screen.width)
                        ShiftObject();
                    break;

                case ExitDirection.Left:        // check right-most point of sprite
                    if ((int)maxSpriteBounds.x + edgeBuffer < 0)
                        ShiftObject();
                    break;

                case ExitDirection.Bottom:      // check lowest point of sprite
                    if ((int)maxSpriteBounds.y + edgeBuffer < 0)
                        ShiftObject();
                    break;

                case ExitDirection.Top:         // check highest point of sprite
                    if ((int)minSpriteBounds.y - edgeBuffer > Screen.height)
                        ShiftObject();
                    break;
            }
        }

        void ShiftObject()
        {
            float randomizer = Random.Range(0f, 3f);

            switch (objectExitSide)
            {
                case ExitDirection.Right:
                    Vector3 screenLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-edgeBuffer, 0, 0));
                    float leftShift = screenLeftEdge.x - (spriteRend.bounds.size.x / 2);
                    transform.position = new Vector3 (leftShift - randomizer, transform.position.y, transform.position.z);
                    break;

                case ExitDirection.Left:
                    Vector3 screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width + edgeBuffer), 0, 0));
                    float rightShift = screenRightEdge.x + (spriteRend.bounds.size.x / 2);
                    transform.position = new Vector3(rightShift + randomizer, transform.position.y, transform.position.z);
                    break;

                case ExitDirection.Bottom:
                    Vector3 screenTopEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height + edgeBuffer), 0));
                    float topShift = screenTopEdge.y + (spriteRend.bounds.size.y / 2);
                    transform.position = new Vector3(transform.position.x, topShift + randomizer, transform.position.z);
                    break;

                case ExitDirection.Top:
                    Vector3 screenBottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, -edgeBuffer, 0));
                    float bottomShift = screenBottomEdge.y - (spriteRend.bounds.size.y / 2);
                    transform.position = new Vector3(transform.position.x, bottomShift - randomizer, transform.position.z);
                    break;
            }
        }
    }
}