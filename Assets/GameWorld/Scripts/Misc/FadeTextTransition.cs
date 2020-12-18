using System.Collections;
using UnityEngine;
using TMPro;

namespace Methodyca.Core
{
    public class FadeTextTransition : MonoBehaviour
    {
        public GameObject transitionDestination;
        public TMP_Text tMPText;


        void OnEnable()
        {
            StartCoroutine(Fader.FadeIn(tMPText));
            
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            yield return new WaitForSeconds(3f);    // 1 second lost on FadeIn
            StartCoroutine(Fader.FadeOut(tMPText));

            yield return new WaitForSeconds(1.2f);    // until FadeOut is finished
            GameManager.instance.GoToRoom(transitionDestination);
        }
    }
}