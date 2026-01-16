using UnityEngine;
using UnityEngine.UIElements;

public class OreTile : MonoBehaviour
{
    [SerializeField] private TileType _tileType;
    private Ground _ground;
    public Vector2Int Position { get; private set; }    

    void Start()
    {
        Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _ground = FindAnyObjectByType<Ground>();
        _ground.GetTile(Position.x, Position.y).Type = _tileType;
    }

}
