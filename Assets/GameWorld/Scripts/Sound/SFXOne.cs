using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    public class SFXOne : MonoBehaviour
    {
        public Sound SFX;

        public void Itsjustkewa()
        {
            SoundManager.instance.PlaySFXOneShot(SFX);
            print("Button Pressed Sound One");
        }
    }
}
