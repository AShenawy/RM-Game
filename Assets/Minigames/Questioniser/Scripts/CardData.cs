using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [CreateAssetMenu(menuName = "GameData/CardData")]
    public class CardData : ScriptableObject
    {
        [SerializeField] new string name;
        [SerializeField] int actionPoint;
        [SerializeField] int interestPoint;
        [SerializeField] int spawnSize;
        [SerializeField] string description;
        [SerializeField] Sprite sprite;
        [SerializeField] Color outlineColor;
        [SerializeField] Question question;

        public string Name => name;
        public int ActionPoint => actionPoint;
        public int InterestPoint => interestPoint;
        public int SpawnSize => spawnSize;
        public string Description => description;
        public Sprite Sprite => sprite;
        public Color OutlineColor => outlineColor;
        public Question Question => question;
    }
}