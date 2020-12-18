namespace Methodyca.Minigames.Protoescape
{
    [System.Serializable]
    public struct EntityCoordinate
    {
        public int Horizontal;
        public int Vertical;

        public EntityCoordinate(int horizontal, int vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
    }
}