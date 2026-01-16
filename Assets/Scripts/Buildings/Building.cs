using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private BuildingStats _stats;
    public UnityEvent BuildingPlaced, Rotated;

    [Range(1, 4)][SerializeField] private int _size = 1;
    public Ground Ground { get; private set; }
    public Vector2Int[] NearPositions { get; private set; }
    public Vector2Int Position { get; private set; }
    public BuildingStats Stats => _stats;
    public Vector2Int Size => new Vector2Int(_size, _size);


    public virtual void Initialize()
    {
        Ground = FindAnyObjectByType<Ground>();
        Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        NearPositions = new Vector2Int[_size * 4];
        int i = 0;
        for (int x = 0; x < _size; x++)
        {
            NearPositions[i] = new Vector2Int(Position.x + x, Position.y - 1);
            i++;
        }
        for (int x = 0; x < _size; x++)
        {
            NearPositions[i] = new Vector2Int(Position.x + x, Position.y + 1);
            i++;
        }
        for (int y = 0; y < _size; y++)
        {
            NearPositions[i] = new Vector2Int(Position.x - 1, Position.y + y);
            i++;
        }
        for (int y = 0; y < _size; y++)
        {
            NearPositions[i] = new Vector2Int(Position.x + 1, Position.y + y);
            i++;
        }

        for (int x = -(Size.x / 2); x < (Size.x / 2) + 1; x++)
        {
            for (int y = -(Size.y / 2); y < (Size.y / 2) + 1; y++)
            {
                Ground.GetTile(Position.x + x, Position.y + y).Building = this;
            }
        }
        BuildingPlaced?.Invoke();
    }

    public void UpdateNeighbours()
    {
        foreach (var position in NearPositions)
        {
            if (Ground.IsTileExist(position.x, position.y))
            {
                var tile = Ground.GetTile(position.x, position.y);
                if (tile.Building != null && tile.Building is IUpdateable updateable)
                {
                    updateable.UpdateObject();
                }
            }
        }
    }

    public virtual void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Rotated?.Invoke();
    }
    public abstract void Destroy();

}
