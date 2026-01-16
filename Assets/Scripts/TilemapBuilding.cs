using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBuilding : MonoBehaviour
{
    [SerializeField] private TileBase _tile;
    [SerializeField] private SpriteRenderer _renderer;
    private bool _isPlaced;

    public void CreateBuidling()
    {
        ConveyorTilemap.Instance.DrawTile(_tile, new Vector2Int((int)transform.position.x, (int)transform.position.y), transform.eulerAngles.z);
        Destroy(_renderer);
        _isPlaced = true;
        //if (_tile is AnimatedTile animatedTile)
        //{
        //    animatedTile
        //}
    }

    public void UpdateRotation()
    {
        ConveyorTilemap.Instance.UpdateRotation(new Vector2Int((int)transform.position.x, (int)transform.position.y), transform.eulerAngles.z);
    }
    private void OnDestroy()
    {
        if (_isPlaced)
            ConveyorTilemap.Instance?.DeleteTile(new Vector2Int((int)transform.position.x, (int)transform.position.y));
    }
}
