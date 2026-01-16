using AYellowpaper.SerializedCollections;
using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildlingStats", menuName = "Scriptable Objects/BuildlingStats")]
public class BuildingStats : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][TextArea] private string _description;
    [SerializeField] private Building _prefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private SerializedDictionary<ItemType, int> _buildingCost = new SerializedDictionary<ItemType, int>();
    [SerializeField] private bool _initOpened;
    private bool _isOpened;

    public IReadOnlyDictionary<ItemType, int> Cost { get; private set; }
    public string Name => _name;
    public string Description => _description;
    public Building Prefab => _prefab;
    public Sprite Icon => _icon;
    public bool IsOpened {get => _isOpened; set => _isOpened = value;}

    public void Initialize()
    {
        _isOpened = _initOpened;
    }

    private void OnValidate()
    {
        Cost = _buildingCost;
    }
}
