using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace LevelData
{
    public class GameLevelManager : IInitializable
    {
        private readonly LevelListSetting _setting;
        private IDictionary<string, LevelItem> _levelIdDic;

        public GameLevelManager(LevelListSetting setting)
        {
            _setting = setting;
        }

        public void Initialize()
        {
            _levelIdDic = _setting.Levels.ToDictionary(x => x.Id);
        }

        public LevelItem this[int i] => _setting.Levels[i];

        public bool Contains(LevelItem item)
        {
            return _setting.Levels.IndexOf(item) > 0;
        }

        public bool ContainsKey(string id)
        {
            return _levelIdDic.ContainsKey(id);
        }

        public int Count => _setting.Levels.Count;

        public string GetSceneNameForId(string levelId)
        {
            return _levelIdDic[levelId].SceneName;
        }

        public List<LevelItem> GetLevelDatas()
        {
            return _setting.Levels;
        }
    }
}