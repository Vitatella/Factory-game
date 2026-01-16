using TMPro;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
    [SerializeField] private Transform _selectionCursor;
    [SerializeField] private TextMeshProUGUI _name, _description;
    [SerializeField] private BuildingCreator _creator;
    [SerializeField] private ItemsCostUI _costUI;
    private SelectBuildingButton _selectedItem;

    public void SelectItem(SelectBuildingButton item)
    {

        if (_selectedItem != null)
        {
            ClearSelection();
        }
        _selectionCursor.position = item.transform.position;
        _selectionCursor.gameObject.SetActive(true);
        _creator.Select(item.Stats);
        _selectedItem = item;
        _costUI.ShowItems(item.Stats.Cost);
        _name.text = item.Stats.Name;
        _description.text = item.Stats.Description;
    }

    public void ClearSelection()
    {
        _selectionCursor.gameObject.SetActive(false);
        _selectedItem = null;
        _costUI.ClearSelection();
        _name.text = "";
        _description.text = "";
    }
}
