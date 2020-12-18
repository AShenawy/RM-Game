using UnityEngine;

namespace Methodyca.Minigames.Observation
{
    public class Spawnable : MonoBehaviour
    {
        [SerializeField] int typeIndex;
        public int TypeIndex => typeIndex;
    }
}