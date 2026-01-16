using Mono.Cecil;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMover : ResourcesMover
{
    [SerializeField] private Ground _ground;
    [SerializeField] private float _conveyorSpeed;
    private List<Item> _resourcesToMove = new List<Item>();
    private List<Vector2> _resourcesStartPoints = new List<Vector2>();
    private List<Vector2> _resourcesFinishPoints = new List<Vector2>();
    private List<Item> _resourceFinishedMoving = new List<Item>();

    public override void AddResource(Item resource, Vector2 startPoint, Vector2 finishPoint)
    {
        _resourcesToMove.Add(resource);
        _resourcesStartPoints.Add(startPoint);
        _resourcesFinishPoints.Add(finishPoint);
    }

    public void RemoveResourceFromMoveList(int i)
    {
        _resourcesToMove.RemoveAt(i);
        _resourcesStartPoints.RemoveAt(i);
        _resourcesFinishPoints.RemoveAt(i);
    }

    private void Update()
    {
        for (int i = 0; i < _resourcesToMove.Count; i++)
        {
            float distance = Vector2.Distance(_resourcesStartPoints[i], _resourcesFinishPoints[i]);
            float currentDistance = Vector2.Distance(_resourcesToMove[i].transform.position, _resourcesStartPoints[i]);
            float currentProgress = currentDistance / distance;
            currentProgress += Time.deltaTime * _conveyorSpeed;
            Vector2 nextPosition = Vector2.Lerp(_resourcesStartPoints[i], _resourcesFinishPoints[i], currentProgress);
            _resourcesToMove[i].transform.position = nextPosition;
            if (currentProgress >= 1)
            {
                var resource = _resourcesToMove[i];
                _resourceFinishedMoving.Add(_resourcesToMove[i]);
                var tile = _ground.GetTile(Mathf.RoundToInt(_resourcesFinishPoints[i].x), Mathf.RoundToInt(_resourcesFinishPoints[i].y));

                RemoveResourceFromMoveList(i);
                if (tile.Building != null && tile.Building is IAcceptResource)
                {
                    (tile.Building as IAcceptResource).AcceptResource(resource);
                }
                
            }
        }
    }


}


public abstract class ResourcesMover : MonoBehaviour
{
    public abstract void AddResource(Item resource, Vector2 startPoint, Vector2 finishPoint);

}