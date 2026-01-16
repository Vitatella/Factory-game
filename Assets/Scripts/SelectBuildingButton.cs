using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private BuildingStats _stats;
    [SerializeField] private BuildingSelection _selection;
    public Button Button => _button;
    public BuildingStats Stats => _stats;

    private void OnValidate()
    {
        _image.sprite = _stats.Icon;
    }

    public void OnClick()
    {
        _selection.SelectItem(this);
    }
}
