namespace GameData
{
    public interface IDataStore
    {
        void PreSave();
        void AfterSave();
    }
}