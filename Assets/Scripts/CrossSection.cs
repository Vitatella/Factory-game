using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CrossSection : Building, IAcceptResource, IResourceInput, IUpdateable
{
    private Item _horizontalItem, _verticalItem;
    private bool IsHorizontalWaiting, IsVerticalWaiting;
    private Vector2Int[] _verticalOutputPositions, _horizontalOutputPositions;
    private bool _isVerticalItemMoving, _isHorizontalItemMoving;
    private List<Item> _movingItems = new List<Item>();
    public override void Initialize()
    {
        base.Initialize();
        _verticalOutputPositions = new Vector2Int[2];
        _horizontalOutputPositions = new Vector2Int[2];
        _verticalOutputPositions[0] = Position + Vector2Int.up;
        _verticalOutputPositions[1] = Position + Vector2Int.down;
        _horizontalOutputPositions[0] = Position + Vector2Int.right;
        _horizontalOutputPositions[1] = Position + Vector2Int.left;
        UpdateNeighbours();
    }

    public void AcceptResource(Item resource)
    {
        _movingItems.Remove(resource);
        if (resource.PreviousPoint.x == Position.x)
        {
            _verticalItem = resource;
            _isVerticalItemMoving = false;
            VerticalMove(resource);
        }
        else
        {
            _horizontalItem = resource;
            _isHorizontalItemMoving = false;
            HorizontalMove(resource);
        }
    }

    private void VerticalMove(Item resource)
    {
        foreach (var outputPosition in _verticalOutputPositions)
        {
            var checkTile = Ground.GetTile(outputPosition.x, outputPosition.y);
            var checkBuilding = checkTile.Building;

            if (checkBuilding != null)
            {
                if (checkBuilding is Conveyor conveyor && conveyor.IsFree && conveyor.OutputPosition != Position)
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    (checkBuilding as Conveyor).Item = resource;
                    _verticalItem = null;

                    if (IsVerticalWaiting)
                    {
                        IsVerticalWaiting = false;
                    }
                    UpdateNeighbours();
                    return;

                }
                else if (checkBuilding is IResourceInput input && input.TryDeliverResource(resource.Type, Position))
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    _verticalItem = null;
                    if (IsVerticalWaiting)
                    {
                        IsVerticalWaiting = false;
                    }
                    UpdateNeighbours();
                    return;
                }
                else
                {
                    IsVerticalWaiting = true;
                }
            }
        }
    }

    private void HorizontalMove(Item resource)
    {
        foreach (var outputPosition in _horizontalOutputPositions)
        {
            var checkTile = Ground.GetTile(outputPosition.x, outputPosition.y);
            var checkBuilding = checkTile.Building;
            if (checkBuilding != null)
            {
                if (checkBuilding is Conveyor conveyor && conveyor.IsFree && conveyor.OutputPosition != Position)
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    (checkBuilding as Conveyor).Item = resource;
                    _horizontalItem = null;

                    if (IsHorizontalWaiting)
                    {
                        IsHorizontalWaiting = false;
                    }
                    UpdateNeighbours();
                    return;

                }
                else if (checkBuilding is IResourceInput input && input.TryDeliverResource(resource.Type, Position))
                {
                    ResourcesController.Instance.SetResourceMoving(resource, Position, checkBuilding.Position);
                    resource.SetPreviousPoint(Position);
                    input.SetMovingItem(resource);
                    _verticalItem = null;
                    if (IsHorizontalWaiting)
                    {
                        IsHorizontalWaiting = false;
                    }
                    UpdateNeighbours();
                    return;
                }
                else
                {
                    IsHorizontalWaiting = true;
                }
            }
        }
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        if (from.x == Position.x)
        {
            if (_verticalItem == null && _isVerticalItemMoving == false)
            {
                _isVerticalItemMoving = true;
                return true;
            }
            return false;
        }
        else
        {
            if (_horizontalItem == null && _isHorizontalItemMoving == false)
            {
                _isHorizontalItemMoving = true;
                return true;
            }
            return false;
        }
    }

    public void UpdateObject()
    {
        if (IsHorizontalWaiting)
        {
            HorizontalMove(_horizontalItem);
        }
        if (IsVerticalWaiting)
        {
            VerticalMove(_verticalItem);
        }
    }

    public void SetMovingItem(Item item)
    {
        _movingItems.Add(item);
    }

    public override void Destroy()
    {
        foreach (var item in _movingItems)
        {
            ResourcesController.Instance.DestroyResource(item);
        }
        Destroy(gameObject);
    }
}
