using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameData
{
    public class GameDataStore : GameDataStoreBase
    {
        public List<LevelDataStore> completedLevels = new List<LevelDataStore>();

        public override void PreSave()
        {
            throw new System.NotImplementedException();
        }

        public override void AfterSave()
        {
            throw new System.NotImplementedException();
        }

        public void CompletedLevel(string levelId, int starsEarned)
        {
            foreach (var level in completedLevels.Where(level => level.Id == levelId))
            {
                level.Stars = Mathf.Max(starsEarned, level.Stars);
                return;
            }

            completedLevels.Add(new LevelDataStore(levelId, starsEarned));
        }

        public bool IsLevelCompleted(string levelId)
        {
            return completedLevels.Any(x => x.Id == levelId);
        }

        public int GetNumOfStarsForLevel(string levelId)
        {
            var level = completedLevels.FirstOrDefault(x => x.Id == levelId);
            return level?.Stars ?? 0;
        }
    }
}