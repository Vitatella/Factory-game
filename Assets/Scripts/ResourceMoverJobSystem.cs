using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class ResourceMoverJobSystem : ResourcesMover
{
    [SerializeField] private Ground _ground;
    [SerializeField] private float _conveyorSpeed;
    private List<Item> _resourcesToMove = new List<Item>();
    private List<float2> _resourcesStartPoints = new List<float2>();
    private List<float2> _resourcesFinishPoints = new List<float2>();
    private List<Item> _resourceFinishedMoving = new List<Item>();
    private List<Transform> _transforms = new List<Transform>();

    private TransformAccessArray _transformAccessArray;
    private NativeArray<float2> _startPositions;
    private NativeArray<float2> _finishPositions;
    private NativeArray<bool> _progress;
    private JobHandle _jobHandle;

    public override void AddResource(Item resource, Vector2 startPoint, Vector2 finishPoint)
    {
        _resourcesToMove.Add(resource);
        _resourcesStartPoints.Add(startPoint);
        _resourcesFinishPoints.Add(finishPoint);
        _transforms.Add(resource.transform);
    }

    public void Update()
    {
        if (_resourcesToMove.Count == 0) return;

        _startPositions = new NativeArray<float2>(_resourcesStartPoints.ToArray(), Allocator.TempJob);
        _finishPositions = new NativeArray<float2>(_resourcesFinishPoints.ToArray(), Allocator.TempJob);
        _progress = new NativeArray<bool>(_resourcesFinishPoints.Count, Allocator.TempJob);
        _transformAccessArray = new TransformAccessArray(_transforms.ToArray(), 10);

        ResourceMoveJob moveJob = new ResourceMoveJob
        {
            StartPositions = _startPositions,
            FinishPositions = _finishPositions,
            ConveyorSpeed = _conveyorSpeed,
            Progress = _progress,
            DeltaTime = Time.deltaTime
        };

        _jobHandle = moveJob.Schedule(_transformAccessArray);
        _jobHandle.Complete();
        for (int i = _progress.Length - 1; i >= 0; i--)
        {
            if (_progress[i] == false) continue;
            var resource = _resourcesToMove[i];
            var tile = _ground.GetTile(Mathf.RoundToInt(_resourcesFinishPoints[i].x), Mathf.RoundToInt(_resourcesFinishPoints[i].y));
            RemoveResourceFromMoveList(i);
            if (tile.Building != null && tile.Building is IAcceptResource)
            {
                (tile.Building as IAcceptResource).AcceptResource(resource);
            }
        }

        _startPositions.Dispose();
        _finishPositions.Dispose();
        _transformAccessArray.Dispose();
        _progress.Dispose();

    }

    public void RemoveResourceFromMoveList(int i)
    {
        _resourcesToMove.RemoveAt(i);
        _resourcesStartPoints.RemoveAt(i);
        _resourcesFinishPoints.RemoveAt(i);
        _transforms.RemoveAt(i);
    }
}
