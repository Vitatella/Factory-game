using UnityEngine;

public class Conveyor : Building, IAcceptResource, IUpdateable
{
    public Vector2Int Direction { get; private set; }
    public Vector2Int OutputPosition { get; private set; }

    public Item Item { get; set; }

    public bool IsFree => Item == null;
    public bool IsWaiting { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        OutputPosition = Position + new Vector2Int(Mathf.RoundToInt(transform.up.x), Mathf.RoundToInt(transform.up.y));
        UpdateNeighbours();
    }


    public void AcceptResource(Item resource)
    {
        TryMoveResource(resource);
    }

    private void TryMoveResource(Item resource)
    {
        if (Ground.IsTileExist(OutputPosition.x, OutputPosition.y) == true)
        {
            var checkTile = Ground.GetTile(OutputPosition.x, OutputPosition.y);
            var checkBuilding = checkTile.Building;
            if (checkBuilding != null)
            {
                if (checkBuilding is Conveyor conveyor && conveyor.IsFree && conveyor.OutputPosition != Position)
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    (checkBuilding as Conveyor).Item = resource;
                    Item = null;
                    if (IsWaiting)
                    {
                        IsWaiting = false;

                    }
                    UpdateNeighbours();

                }
                else if (checkBuilding is IResourceInput input && input.TryDeliverResource(resource.Type, Position))
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    input.SetMovingItem(resource);
                    Item = null;
                    if (IsWaiting)
                    {
                        IsWaiting = false;

                    }
                    UpdateNeighbours();
                }
                else
                {
                    IsWaiting = true;
                }
            }
            else
            {
                IsWaiting = true;
            }
        }
        else
        {
            IsWaiting = true;
        }
    }

    public void UpdateObject()
    {
        if (IsWaiting)
        {
            TryMoveResource(Item);
        }
    }

    public override void Rotate(float angle)
    {
        base.Rotate(angle);
        OutputPosition = Position + new Vector2Int(Mathf.RoundToInt(transform.up.x), Mathf.RoundToInt(transform.up.y));
        UpdateObject();
    }

public override void Destroy()
    {
        Debug.Log(Item);
        if (Item != null) ResourcesController.Instance.DestroyResource(Item);
        Destroy(gameObject);
    }
}
