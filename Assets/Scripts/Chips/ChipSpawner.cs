using System;
using Tiles;
using UnityEngine;
using static Chips.Chip;
using Random = UnityEngine.Random;

namespace Chips
{
    public class ChipSpawner : MonoBehaviour
    {
        [SerializeField] private Chip chipPrefab;
        [SerializeField] private ChipColorsConfigContainer chipColorsConfigContainer;


        private int _colorsCount = -1;

        private int ColorsCount
        {
            get
            {
                if (_colorsCount == -1) _colorsCount = Enum.GetNames(typeof(ChipColor)).Length;
                return _colorsCount;
            }
        }


        public Chip[] GetChips(int chipsCount)
        {
            var sameColorChipsCount = chipsCount / ColorsCount;
            var chips = new Chip[chipsCount];
            var colorsChips = new int[ColorsCount];
            bool colorFound;
            int random;

            for (int i = 0; i < chipsCount; i++)
            {
                colorFound = false;
                while (!colorFound)
                {
                    random = Random.Range(0, ColorsCount);
                    if (colorsChips[random] < sameColorChipsCount)
                    {
                        colorsChips[random]++;
                        chips[i] = GetNewChip((ChipColor) random);
                        colorFound = true;
                    }
                    else
                    {
                        var isAvaiableToAdd = false;
                        foreach (var colorsChip in colorsChips)
                        {
                            if (colorsChip < sameColorChipsCount) isAvaiableToAdd = true;
                        }

                        if (!isAvaiableToAdd) throw new Exception("Could not generate chips");
                    }
                }
            }

            return chips;
        }

        private Chip GetNewChip(ChipColor color)
        {
            var newChip = Instantiate(chipPrefab);

            newChip.SetUpColorConfig(chipColorsConfigContainer[color]);
            return newChip;
        }
    }
}