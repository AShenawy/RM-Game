using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    public class VerticalOscillator : MonoBehaviour
    {
        public float minY;
        public float maxY;
        public float speed;     // controls how fast the oscillation happens
        
        RectTransform rect;
        float t = 0f;

        void Start()
        {
            rect = GetComponent<RectTransform>();
        }

        void Update()
        {
            Oscillate();

            t += speed * Time.deltaTime;

            // if t is higher than 1 then swap min and max to move in opposite direction
            if (t > 1f)
            {
                float temp = maxY;
                maxY = minY;
                minY = temp;
                t = 0f;
            }
        }

        public void Oscillate()
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Mathf.Lerp(minY, maxY, t));
        }
    }
}