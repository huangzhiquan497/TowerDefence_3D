using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIPanel
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _levelSelectBtn;


        [Inject] private LevelSelectPanel.Factory _levelPanelFactory;

        private void Start()
        {
            _levelSelectBtn.onClick.AddListener(ShowLevelListPanel);
        }

        private void ShowLevelListPanel()
        {
            _levelPanelFactory.Create();
        }
    }
}