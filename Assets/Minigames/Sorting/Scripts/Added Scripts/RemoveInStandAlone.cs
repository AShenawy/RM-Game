using UnityEngine;

namespace Methodyca.Minigames
{
    // this script destroys game object if minigame will be built as standalone not in main game
    public class RemoveInStandAlone : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            Destroy(gameObject);
        }
    }
}