using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract Item GetResource(Vector2 position, ItemType type);

}
