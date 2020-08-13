using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] GameObject root;

        void Start()
        {
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            root.SetActive(true);
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnGameOver -= GameOverHandler;
        }
    }
}