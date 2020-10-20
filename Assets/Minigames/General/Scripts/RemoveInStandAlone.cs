using UnityEngine;

namespace Methodyca.Minigames
{
    // this script destroys game object if minigame will be built as standalone not in main game
    // mainly for buttons that return player to main game, which aren't needed in the standalone minigame version
    public class RemoveInStandAlone : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            Destroy(gameObject);
        }
    }
}