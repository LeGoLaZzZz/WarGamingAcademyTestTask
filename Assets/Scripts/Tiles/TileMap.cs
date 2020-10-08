using System;
using Chips;
using Tiles;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class AnyChipSwapped : UnityEvent
{
}


public class TileMap : MonoBehaviour
{
    public Tile[,] tiles;

    [SerializeField] private int height;
    [SerializeField] private int width;

    public AnyChipSwapped anyChipSwapped = new AnyChipSwapped();

    public int Height
    {
        get => height;
        private set => height = value;
    }

    public int Width
    {
        get => width;
        private set => width = value;
    }

    public void InstantiateTileArrayTable(int height, int width)
    {
        tiles = new Tile[width, height];
        this.Height = height;
        this.Width = width;
    }


    public bool TrySwapTiles(Tile tile1, Tile tile2)
    {
        if (tile1.IsBlocked || tile2.IsBlocked) return false;
        if (tile1.IsEmpty && tile2.IsEmpty) return false;
        if (!CheckIfTilesNeighbours(tile1, tile2)) return false;


        //TODO Дублирование кода?
        if (tile1.IsChiped && tile2.IsEmpty)
        {
            tile1.TryGetChip(out var chip);
            SwapChipOnTile(chip, tile2);
            return true;
        }


        if (tile1.IsEmpty && tile2.IsChiped)
        {
            tile2.TryGetChip(out var chip);
            SwapChipOnTile(chip, tile1);
            return true;
        }


        if (tile1.IsChiped && tile2.IsChiped)
        {
            tile1.TryGetChip(out var chip1);
            tile2.TryGetChip(out var chip2);
            SwapChips(chip1, chip2);

            return true;
        }


        return false;
    }


    private void Awake()
    {
        InstantiateTileArrayTable(Height, Width);
        var componentsInChildren = transform.GetComponentsInChildren<Tile>();

        foreach (var tile in componentsInChildren)
        {
            tiles[tile.X, tile.Y] = tile;
        }
    }


    private bool CheckIfTilesNeighbours(Tile tile1, Tile tile2)
    {
        return Mathf.Abs(tile1.X - tile2.X) == 1 && (tile1.Y == tile2.Y) ||
               (tile1.X == tile2.X && Mathf.Abs(tile1.Y - tile2.Y) == 1);
    }

    private void SwapChips(Chip chip1, Chip chip2)
    {
        chip1.Tile.SetChiped(chip2);
        chip2.Tile.SetChiped(chip1);

        var tile2 = chip2.tile;

        chip2.SetOnTile(chip1.Tile);
        chip1.SetOnTile(tile2);

        anyChipSwapped.Invoke();
    }

    private void SwapChipOnTile(Chip chip, Tile tile)
    {
        if (!tile.IsEmpty) throw new Exception("Cant Swipe tile Is not Empty");
        chip.Tile.SetEmpty();
        tile.SetChiped(chip);
        chip.SetOnTile(tiles[tile.X, tile.Y]);

        anyChipSwapped.Invoke();
    }
}