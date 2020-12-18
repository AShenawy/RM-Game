using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{ 
    public class SFXOne : MonoBehaviour
    {
        public Sound SFX;
        public void Itsjustkewa()
        {
            SoundManager.instance.PlaySFXOneShot(SFX);
        }
    }
}