using UnityEngine;
using UnityEngine.Pool;

public class Item : MonoBehaviour, IResource
{
    [SerializeField] private SpriteRenderer _renderer;
    private IObjectPool<Item> _pool;
    public ItemType Type { get; private set; }
    public Vector2 PreviousPoint { get; private set; }

    public void SetPool(IObjectPool<Item> pool)
    {
        _pool = pool;
    }

    public void Initialize(Vector2 position, ItemType type)
    {
        transform.position = position;
        Type = type;
        _renderer.sprite = Type.Sprite;
    }

    public void SetPreviousPoint(Vector2 position)
    {
        PreviousPoint = position;
    }

    public void ReturnToPool()
    {
        _pool.Release(this);
    }
}
