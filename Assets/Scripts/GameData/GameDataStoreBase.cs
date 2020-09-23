namespace GameData
{
    public abstract class GameDataStoreBase : IDataStore
    {
        public float MasterVolume = 1f;
        public float SfxVolume = 1f;
        public float MusicVolume = 1f;
        public abstract void PreSave();

        public abstract void AfterSave();
    }
}