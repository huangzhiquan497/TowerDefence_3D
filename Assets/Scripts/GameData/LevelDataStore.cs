namespace GameData
{
    public class LevelDataStore
    {
        public string Id;
        public int Stars;

        public LevelDataStore(string id, int stars)
        {
            Id = id;
            Stars = stars;
        }
    }
}