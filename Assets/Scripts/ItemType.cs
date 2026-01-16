using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "Scriptable Objects/ItemType")]
public class ItemType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public Sprite Sprite => _sprite;
}
