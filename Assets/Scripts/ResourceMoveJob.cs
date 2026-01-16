using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct ResourceMoveJob : IJobParallelForTransform
{
    [ReadOnly] public float ConveyorSpeed;
    [ReadOnly] public NativeArray<float2> StartPositions;
    [ReadOnly] public NativeArray<float2> FinishPositions;
    [ReadOnly] public float DeltaTime;


    [NativeDisableParallelForRestriction]
    public NativeArray<bool> Progress;


    public void Execute(int index, TransformAccess transform)
    {
        float distance = math.distance(StartPositions[index], FinishPositions[index]);
        float currentDistance = math.distance(new float2(transform.position.x, transform.position.y), StartPositions[index]);
        float currentProgress = currentDistance / distance;
        currentProgress += DeltaTime * ConveyorSpeed;
        Vector2 nextPosition = Vector2.Lerp(StartPositions[index], FinishPositions[index], currentProgress);
        transform.position = nextPosition;
        Progress[index] = currentProgress >= 1;
    }

}
