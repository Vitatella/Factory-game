using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Turret : Building, IAcceptResource, IResourceInput
{
    [SerializeField] private Transform _turret;
    [SerializeField] private int _ammoMultiplier = 1;
    [SerializeField] private SerializedDictionary<ItemType, float> _ammoDamage = new SerializedDictionary<ItemType, float>();
    [SerializeField] private TurretShooting _shooting;
    [SerializeField] private LayerMask _shootingMask;
    [SerializeField] private float _shootingDistance = 15f;
    [SerializeField] private TargetFinder _targetFinder;
    private ItemType _movingItem;
    private int _shotsCount;

    private ItemType _ammo;
        private List<Item> _movingItems = new List<Item>();

    public void AcceptResource(Item resource)
    {
        _movingItems.Remove(resource);
        _movingItem = null;
        _ammo = resource.Type;
        _shotsCount = _ammoMultiplier;
        TryReload();
        ResourcesController.Instance.DestroyResource(resource);
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateNeighbours();
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        if (_ammoDamage.ContainsKey(resource) && _movingItem == null && _ammo == null)
        {
            _movingItem = resource;
            return true;
        }
        return false;
    }

   

    private void Update()
    {
        if (_targetFinder.Target != null)
        {
            var dir = (Vector2)_targetFinder.Target.position - (Vector2)transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _turret.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            if (_shooting.IsLoaded)
            {
                _shooting.Shoot(_ammoDamage[_ammo]);
                TryReload();
            }
        }
    }

    private void TryReload()
    {
        if (_shooting.IsReloading == true) return;
        if (_shotsCount > 0)
        {
            _shooting.Reload();
            _shotsCount--;
        }
        else
        {
            _ammo = null;
            UpdateNeighbours();
        }
    }

    

    private bool CheckTarget(Transform target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, _shootingDistance, _shootingMask);
        return hit.collider == target;
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
