using System;
using Chips;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PuzzleSolved : UnityEvent
{
}


[RequireComponent(typeof(TileMap))]
public class PuzzleCompleteChecker : UnityEngine.MonoBehaviour
{
    [SerializeField] private WinConditionsContainer winConditionsContainer;
    private TileMap _tileMap;

    public static PuzzleSolved puzzleSolved = new PuzzleSolved();

    private void OnEnable()
    {
        _tileMap.anyChipSwapped.AddListener(OnAnyChipSwapped);
    }

    private void Awake()
    {
        _tileMap = GetComponent<TileMap>();
    }

    private void OnDisable()
    {
        _tileMap.anyChipSwapped.RemoveListener(OnAnyChipSwapped);
    }

    private void OnAnyChipSwapped()
    {
        foreach (var winCondition in winConditionsContainer.winConditions)
            if (!CheckRow(winCondition))
                return;

        PuzzleSolved();
    }

    private void PuzzleSolved()
    {
        puzzleSolved.Invoke();
    }

    private bool CheckRow(WinConditionsContainer.RowColor rowColor)
    {
        for (int i = 0; i < _tileMap.Width; i++)
        {
            if (_tileMap.tiles[rowColor.rowNum, i].TryGetChip(out var chip))
            {
                if (chip.CurrentChipColor != rowColor.chipColor) return false;
            }
            else
                return false;
        }

        return true;
    }
}