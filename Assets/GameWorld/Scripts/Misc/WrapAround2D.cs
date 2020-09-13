using UnityEngine;

// This scripts teleports a game object to the opposite side of the screen once it exits it (for 2D assets)
namespace Methodyca.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WrapAround2D : MonoBehaviour
    {
        private enum EdgeDirection { Right, Left, Top, Bottom};
        [SerializeField] private EdgeDirection wrapAfterObjectEdge;
        public int edgeBuffer = 10;

        SpriteRenderer spriteRend;
        Vector3 distanceOutMin;
        Vector3 distanceOutMax;

        // Start is called before the first frame update
        void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            distanceOutMin = Camera.main.WorldToScreenPoint(spriteRend.bounds.min);
            distanceOutMax = Camera.main.WorldToScreenPoint(spriteRend.bounds.max);

            CheckBoundaryConditions();
        }

        void CheckBoundaryConditions()
        {
            switch (wrapAfterObjectEdge)
            {
                case EdgeDirection.Left:
                    if ((int)distanceOutMin.x - edgeBuffer > Screen.width)
                        ShiftObject();
                    break;

                case EdgeDirection.Right:
                    if ((int)distanceOutMax.x + edgeBuffer < 0)
                        ShiftObject();
                    break;

                case EdgeDirection.Top:
                    if ((int)distanceOutMax.y + edgeBuffer < 0)
                        ShiftObject();
                    break;

                case EdgeDirection.Bottom:
                    if ((int)distanceOutMin.y - edgeBuffer > Screen.height)
                        ShiftObject();
                    break;
            }

        }

        void ShiftObject()
        {
            float randomizer = Random.Range(0f, 3f);

            switch (wrapAfterObjectEdge)
            {
                case EdgeDirection.Left:
                    Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-edgeBuffer, 0, 0));
                    float leftShift = leftEdge.x - (spriteRend.bounds.size.x / 2);
                    transform.position = new Vector3 (leftShift - randomizer, transform.position.y, transform.position.z);
                    break;

                case EdgeDirection.Right:
                    Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width + edgeBuffer), 0, 0));
                    float rightShift = rightEdge.x + (spriteRend.bounds.size.x / 2);
                    transform.position = new Vector3(rightShift + randomizer, transform.position.y, transform.position.z);
                    break;

                case EdgeDirection.Top:
                    Vector3 topEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height + edgeBuffer), 0));
                    float topShift = topEdge.y + (spriteRend.bounds.size.y / 2);
                    transform.position = new Vector3(transform.position.x, topShift + randomizer, transform.position.z);
                    break;

                case EdgeDirection.Bottom:
                    Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, -edgeBuffer, 0));
                    float bottomShift = bottomEdge.y - (spriteRend.bounds.size.y / 2);
                    transform.position = new Vector3(transform.position.x, bottomShift - randomizer, transform.position.z);
                    break;
            }
        }
    }
}