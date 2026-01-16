using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private float _scrollMultiplier = 1;
    [SerializeField] private float _cameraMinSize, _cameraMaxSize;
    private Vector2 _deltaPosition;
    [SerializeField] private Camera _camera;
    private bool _isFollowingEnabled = true;

    private void OnValidate()
    {
        if (_cameraMinSize > _cameraMaxSize)
        {
            _cameraMaxSize = _cameraMinSize;
        }
    }

    void Start()
    {
        _camera.transform.position = transform.position;
    }

    public void EnableFollowing()
    {
        _isFollowingEnabled = true;
    }

    public void DisableFollowing()
    {
        _isFollowingEnabled = false;
    }


    private void FixedUpdate()
    {
        Vector2 targetPosition;
        Vector2 cameraPosition = _camera.transform.position;
        targetPosition = transform.position;

        Vector2 deltaPosition = targetPosition - cameraPosition;
        _deltaPosition = cameraPosition - (Vector2)transform.position;
        Vector2 nextPos = cameraPosition + deltaPosition * _multiplier * Time.fixedDeltaTime;
        _camera.transform.position = new Vector3(nextPos.x, nextPos.y, -10f);
        if (Input.mouseScrollDelta.y != 0)
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + Input.mouseScrollDelta.y * _scrollMultiplier, _cameraMinSize, _cameraMaxSize);
        }
    }

    public void ChangePosition(Vector2 position)
    {
        _camera.transform.position = new Vector3(position.x + _deltaPosition.x, position.y + _deltaPosition.y, -10f);
    }
}
