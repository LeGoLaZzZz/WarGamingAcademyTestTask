using System;
using System.Collections.Generic;
using Chips;
using Tiles;
using Tiles.TileStates;
using UnityEngine;

[RequireComponent(typeof(TileMap))]
[RequireComponent(typeof(ChipSpawner))]
public class TilesSetter : MonoBehaviour
{
    private TileMap _tileMap;
    private ChipSpawner _chipSpawner;

    private void Awake()
    {
        _chipSpawner = GetComponent<ChipSpawner>();
        _tileMap = GetComponent<TileMap>();
    }

    private void Start()
    {
        SetUpTiles(_tileMap);
    }


    public void SetUpTiles(TileMap tileMap)
    {
        var chippedTiles = new List<Tile>();

        foreach (var tile in tileMap.tiles)
        {
            switch (tile.StartState)
            {
                case TileState.TileStateType.TileBlocked:

                    tile.SetBlocked();
                    break;

                case TileState.TileStateType.TileEmpty:

                    tile.SetEmpty();
                    break;

                case TileState.TileStateType.TileChipped:

                    chippedTiles.Add(tile);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        var chips = _chipSpawner.GetChips(chippedTiles.Count);
        var chipsPointer = 0;

        foreach (var chippedTile in chippedTiles)
        {
            chips[chipsPointer].SetOnTile(chippedTile, false);
            chippedTile.SetChiped(chips[chipsPointer]);
            chipsPointer++;
        }
    }
}