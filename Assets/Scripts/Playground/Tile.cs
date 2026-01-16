using UnityEngine;

public enum TileType
{
    Nothing,
    Copper,
    Titanium,
    Iron,
    Coal,
    Water,
    Mountain
}
public class Tile
{
    private Vector2Int _position;
    [SerializeField] private TileType _type;
    private Building _buidling;

    public Tile(int x, int y)
    {
        _position = new Vector2Int(x, y);
    }
    public Building Building
    {
        get => _buidling; set => _buidling = value;
    }
    public TileType Type
    {
        get => _type; set => _type = value;
    }
}

