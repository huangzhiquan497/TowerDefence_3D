using GameData;
using LevelData;
using UIPanel;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private LevelListSetting _levelListSetting;
    [SerializeField] private GameObject _loader;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameLevelManager>().AsSingle().WithArguments(_levelListSetting);
        Container.BindInterfacesAndSelfTo<GameDataManager>().AsSingle();
        Container.BindFactory<LevelSelectPanel, LevelSelectPanel.Factory>().FromComponentInNewPrefab(_levelSelectPanel)
            .AsSingle().MoveIntoAllSubContainers();

        Container.BindFactory<LoaderBusyIndicator, LoaderBusyIndicator.Factory>().FromComponentInNewPrefab(_loader)
            .CopyIntoAllSubContainers();
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoaderBusy>().AsSingle();
    }
}