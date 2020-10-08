using System;
using System.Collections.Generic;
using UnityEngine;
using static Chips.Chip;

namespace Chips
{
    [CreateAssetMenu(fileName = "chipColorsConfigContainer", menuName = "ChipColorsConfigContainer", order = 0)]
    public class ChipColorsConfigContainer : ScriptableObject
    {
        [Serializable]
        public class ChipColorConfig
        {
            public Sprite colorSprite;
            public ChipColor chipColor;
        }

        [SerializeField] private ChipColorConfig[] chipColorConfigs;

        private Dictionary<ChipColor, ChipColorConfig> _chipColorConfigs;


        public ChipColorConfig this[ChipColor chipColor]
        {
            get
            {
                if (_chipColorConfigs == null) GenerateColorDictionary();
                return _chipColorConfigs[chipColor];
            }
        }

        private void GenerateColorDictionary()
        {
            _chipColorConfigs = new Dictionary<ChipColor, ChipColorConfig>();

            foreach (var chipColorConfig in chipColorConfigs)
            {
                _chipColorConfigs[chipColorConfig.chipColor] = chipColorConfig;
            }
        }
    }
}