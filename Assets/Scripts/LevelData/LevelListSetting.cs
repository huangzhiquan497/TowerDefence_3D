using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [CreateAssetMenu(fileName = "LevelListSetting", menuName = "GameKit/LevelListSetting")]
    public class LevelListSetting : ScriptableObject
    {
        public List<LevelItem> Levels;
    }
}