using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorTilemap : Singleton<ConveyorTilemap>
{
    [SerializeField] private Tilemap _tileMap;

    public void DrawTile(TileBase tile, Vector2Int position, float rotation)
    {
        _tileMap.SetTile(_tileMap.WorldToCell((Vector2)position), tile);
        _tileMap.AddTileAnimationFlags(_tileMap.WorldToCell((Vector2)position), TileAnimationFlags.SyncAnimation);
        //_tileMap.SetAnimationTime((Vector3Int)position, Time.unscaledTime);
        UpdateRotation(position, rotation);
        //_tileMap.SetAnimationFrame(_tileMap.WorldToCell((Vector2)position), (tile as AnimatedTile).m_AnimationStartFrame);
    }

    public void UpdateRotation(Vector2Int position, float rotation)
    {
        _tileMap.SetTransformMatrix((Vector3Int)position, Matrix4x4.Rotate(Quaternion.Euler(0, 0, rotation)));
    }

    public void DeleteTile(Vector2Int position)
    {
        _tileMap.SetTile(_tileMap.WorldToCell((Vector2)position), null);
       
    }
}
