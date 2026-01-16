using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    private Tile[,] _tiles;
    [SerializeField] private Vector2Int _size;
    private Tilemap _tilemap;

    private void Awake()
    {
        _tiles = new Tile[_size.x, _size.y];
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                _tiles[x,y] = new Tile(x, y);
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return _tiles[x,y];
    }

    public bool IsTileExist(int x, int y)
    {
        if ((x < 0 || x >= _size.x) || (y < 0 || y >= _size.y)) return false;
        return _tiles[x, y] != null;
    }
}
