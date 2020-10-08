using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private ChipsPicker chipsPicker;


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        PuzzleCompleteChecker.puzzleSolved.AddListener(OnPuzzleSolved);
    }

    private void Start()
    {
        winCanvas.enabled = false;
    }

    private void OnDisable()
    {
        PuzzleCompleteChecker.puzzleSolved.RemoveListener(OnPuzzleSolved);
    }


    private void OnPuzzleSolved()
    {
        chipsPicker.CanPick = false;
        winCanvas.enabled = true;
    }
}