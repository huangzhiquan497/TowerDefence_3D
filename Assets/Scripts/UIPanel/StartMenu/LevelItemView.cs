using System;
using LevelData;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    public class LevelItemView : MonoBehaviour
    {
        [SerializeField] private Button _enterBtn;
        [SerializeField] private Text _titleDisplay;

        [SerializeField] private Text _description;

        [SerializeField] private Sprite _starAchieved;

        [SerializeField] private Image[] _stars;

        public void SetData(LevelItem data, int starts, Action<string> clickCallback)
        {
            _titleDisplay.text = data.Name;
            _description.text = data.Description;
            for (var i = 0; i < starts; i++)
            {
                _stars[i].overrideSprite = _starAchieved;
            }

            _enterBtn.onClick.AddListener(() => clickCallback?.Invoke(data.Id));
        }
    }
}