using System.Collections;
using System.Collections.Generic;
using Chips;
using UnityEngine;

public class WinConditionView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer chipView;

    public void SetUpView(ChipColorsConfigContainer.ChipColorConfig colorConfig)
    {
        chipView.sprite = colorConfig.colorSprite;
    }
}