using UnityEngine;

public interface IResourceInput
{
    public bool TryDeliverResource(ItemType resource, Vector2Int from);
    public void SetMovingItem(Item item);

}
