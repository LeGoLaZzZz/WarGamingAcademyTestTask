using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEditor;
using UnityEngine;


public class TileMapGenerator : MonoBehaviour
{
    public Transform upLeftPoint;
    public Vector2 padding = new Vector2(1, 1);
    public Vector2Int tileMapSize = new Vector2Int(5, 5);

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileMap generatedMap;

    private Vector3 _calculatedPosition;

    public void GenerateTileMap(int size) => GenerateTileMap(size, size);

    public void GenerateTileMap(int height, int width)
    {
        var tileSize = tilePrefab.GetComponentInChildren<SpriteRenderer>().size;
        _calculatedPosition = upLeftPoint.position;


        if (generatedMap == null)
        {
            generatedMap = new GameObject("TileMap").AddComponent<TileMap>();
            generatedMap.transform.position = upLeftPoint.position;
            generatedMap.InstantiateTileArrayTable(height, width);
        }

        for (var y = 0; y < height; y++)
        {
            _calculatedPosition.x = upLeftPoint.position.x;
            for (var x = 0; x < width; x++)
            {
                var spawnTile = SpawnTile(x, y, generatedMap.transform);
                spawnTile.transform.position = _calculatedPosition;
                _calculatedPosition.x += padding.x + tileSize.x;
            }

            _calculatedPosition.y -= (padding.y + tileSize.y);
        }
    }

    private Tile SpawnTile(int x, int y, Transform map)
    {
        Tile newTile = (Tile) PrefabUtility.InstantiatePrefab(tilePrefab);
        newTile.transform.SetParent(map, true);
        newTile.Configure(x, y);
        return newTile;
    }
}