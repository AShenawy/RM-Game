using System.Collections;
using UnityEngine;

namespace Methodyca.Core
{
    // This script creates a flicker effect on objects making them active/inactive randomly
    public class SpriteFlicker : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            float random = Random.Range(0.2f, 3f);
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

            print("Coroutine started");

            yield return new WaitForSecondsRealtime(random);

            print("Done waiting");
            yield return StartCoroutine(Flicker());
        }
    }
}