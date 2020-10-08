using System;
using Chips;
using Tiles;
using Tiles.TileStates;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TileMap))]
public class ChipsPicker : MonoBehaviour
{
    private Tile _firstPicked;
    private TileMap _tileMap;
    private Camera _camera;
    private bool _canPick = true;

    public bool CanPick
    {
        get => _canPick;
        set => _canPick = value;
    }


    private void Awake()
    {
        _tileMap = GetComponent<TileMap>();
        _camera = Camera.main;
        CanPick = true;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanPick)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Tile>(out var tile))
                {
                    OnAnyTileClick(tile);
                }
            }
        }
    }


    private void OnAnyTileClick(Tile tile)
    {
        if (_firstPicked != null)
        {
            if (tile.IsBlocked) return;
            _firstPicked.SetChosen(false);
            _tileMap.TrySwapTiles(_firstPicked, tile);
            _firstPicked = null;
        }
        else
        {
            if (tile.IsBlocked || tile.IsEmpty) return;

            _firstPicked = tile;
            _firstPicked.SetChosen(true);
        }
    }
}