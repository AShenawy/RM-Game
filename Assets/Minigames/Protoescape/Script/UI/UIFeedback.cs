using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIFeedback : MonoBehaviour
    {
        private void OnEnable()
        {
            PrototypeTester.OnPrototypeTested += PrototypeTestedHandler;
        }

        private void PrototypeTestedHandler(int current, int total)
        {

        }

        private void OnDisable()
        {
            PrototypeTester.OnPrototypeTested -= PrototypeTestedHandler;
        }
    }
}