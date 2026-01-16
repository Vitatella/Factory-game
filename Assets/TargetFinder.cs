using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask, _raycastMask;
    private List<Transform> _targets = new List<Transform>();
    private Transform _currentTarget;

    private float _timeToUpdateTarget = 0.5f;

    public Transform Target => _currentTarget;
    public Vector2 LastTargetPosition { get; private set; }

    private void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    private IEnumerator UpdateTarget()
    {
        while (true)
        {
            FindCloseTarget();
            if (_currentTarget != null)
                LastTargetPosition = _currentTarget.position;
            yield return new WaitForSeconds(_timeToUpdateTarget);
        }
    }

    private void FindCloseTarget()
    {
        if (_targets.Count == 0)
        {
            _currentTarget = null;
        }
        else if (_targets.Count == 1)
        {
            _currentTarget = CheckTarget(_targets[0]) ? _targets[0] : null;
        }
        else
        {
            _targets = _targets.OrderBy(target => Vector2.Distance(transform.position, target.position)).ToList();
            for (int i = 0; i < _targets.Count; i++)
            {
                if (CheckTarget(_targets[i]))
                {
                    _currentTarget = _targets[i];
                    return;
                }
            }
        }
    }

    private bool CheckTarget(Transform target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, 20, _raycastMask);
        if (hit.collider != null)
        {
            return hit.collider.transform == target;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _targets.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _targets.Remove(collision.transform);
    }
}
