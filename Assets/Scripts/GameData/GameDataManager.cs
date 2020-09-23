using System;
using LevelData;
using UnityEngine;
using Zenject;

namespace GameData
{
    public class GameDataManager : IInitializable
    {
        private readonly GameLevelManager GameLevelManager;

        private GameDataStore GameDataStore;
        private JsonSaver<GameDataStore> _dataSaver;
        private const string _fileName = "tower_save";

        public GameDataManager(GameLevelManager gameLevelManager)
        {
            GameLevelManager = gameLevelManager;
        }

        public void Initialize()
        {
#if UNITY_EDITOR
            _dataSaver = new JsonSaver<GameDataStore>(_fileName);
#else
            _dataSaver = new EncryptedJsonSaver<GameDataStore>(_fileName);
#endif

            LoadData();
        }


        public void CompletedLevel(string levelId, int starsEarned)
        {
            if (!GameLevelManager.ContainsKey(levelId))
            {
                Debug.LogWarning($"dont exit current level {levelId}");
                return;
            }

            GameDataStore.CompletedLevel(levelId, starsEarned);
            SaveData();
        }

        public int GetStarsForLevel(string levelId)
        {
            if (!GameLevelManager.ContainsKey(levelId))
            {
                Debug.LogWarning($"cannot check with level {levelId}");
                return 0;
            }

            return GameDataStore.GetNumOfStarsForLevel(levelId);
        }

        private void SaveData()
        {
            _dataSaver.Save(GameDataStore);
        }

        private void LoadData()
        {
            try
            {
                if (!_dataSaver.Load(out GameDataStore))
                {
                    GameDataStore = new GameDataStore();
                    SaveData();
                }
            }
            catch (Exception)
            {
                Debug.LogWarning("failed to load data,resetting");
                GameDataStore = new GameDataStore();
                SaveData();
            }
        }
    }
}