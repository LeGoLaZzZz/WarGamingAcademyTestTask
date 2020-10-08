using System;
using System.Collections;
using System.Collections.Generic;
using Chips;
using UnityEngine;

public class WinConditionViewer : MonoBehaviour
{
    [SerializeField] private WinConditionsContainer winConditionsContainer;
    [SerializeField] private ChipColorsConfigContainer chipColorsConfigContainer;
    [SerializeField] private WinConditionView winConditionViewPrefab;
    [SerializeField] private TileMap tileMap;

    [SerializeField] private Vector3 padding = new Vector3(0, 0.1f, 0);

    private void Start()
    {
        foreach (var winCondition in winConditionsContainer.winConditions)
        {
            var newView = Instantiate(winConditionViewPrefab);
            newView.SetUpView(chipColorsConfigContainer[winCondition.chipColor]);

            newView.transform.position = tileMap.tiles[winCondition.rowNum, 0].transform.position + padding;
        }
    }
}