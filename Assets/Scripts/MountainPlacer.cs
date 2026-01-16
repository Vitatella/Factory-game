using UnityEngine;
using UnityEngine.Tilemaps;

public class MountainPlacer : MonoBehaviour
{
    [SerializeField] private Tilemap _mountainTilemap;
    [SerializeField] private Ground _ground;
    void Start()
    {
        for (int x = _mountainTilemap.cellBounds.xMin; x < _mountainTilemap.cellBounds.xMax; x++)
        {
            for (int y = _mountainTilemap.cellBounds.yMin; y < _mountainTilemap.cellBounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = _mountainTilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    if (_ground.IsTileExist(x, y))
                    {
                        _ground.GetTile(x, y).Type = TileType.Mountain;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
