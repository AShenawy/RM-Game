using System.Collections;
using UnityEngine;

namespace Methodyca.Core
{
    // This script creates a flicker effect on objects making them active/inactive randomly
    public class SpriteFlicker : MonoBehaviour
    {
        // Starting the coroutine in start isn't working.
        // Possibly when GameManager disables all rooms this blocks the operation
        private void OnEnable()
        {
            StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            // reverse the sprite component active/inactive to imitate light on/off
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

            // get a random time for the waiting to make flickering realistic
            float randomTimer = Random.Range(0.2f, 3f);
            yield return new WaitForSecondsRealtime(randomTimer);

            // restart the coroutine to keep and endless play loop of effect
            yield return StartCoroutine(Flicker());
        }
    }
}