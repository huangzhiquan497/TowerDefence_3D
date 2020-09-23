using GameData;
using LevelData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UIPanel
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private LevelItemView _prefab;
        [SerializeField] private Transform _container;

        [Inject] private GameDataManager _dataManager;
        [Inject] private GameLevelManager _gameLevelManager;
        [Inject] private ISceneLoader _sceneLoader;

        private void Start()
        {
            InitPanel();
        }

        private void InitPanel()
        {
            var datas = _gameLevelManager.GetLevelDatas();
            foreach (var data in datas)
            {
                var item = Instantiate(_prefab, _container);
                item.SetData(data,  _dataManager.GetStarsForLevel(data.Id), ChangeScene);
            }
        }

        private void ChangeScene(string levelId)
        {
            _sceneLoader.LoadScene(_gameLevelManager.GetSceneNameForId(levelId));
        }


        public class Factory : PlaceholderFactory<LevelSelectPanel>
        {
        }
    }
}