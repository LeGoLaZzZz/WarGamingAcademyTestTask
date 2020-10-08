using System;
using Chips;

[UnityEngine.CreateAssetMenu(fileName = "WinConditions", menuName = "WinConditions", order = 0)]
public class WinConditionsContainer : UnityEngine.ScriptableObject
{
    [Serializable]
    public class RowColor
    {
        public int rowNum;
        public Chip.ChipColor chipColor;
    }


    public RowColor[] winConditions;
}