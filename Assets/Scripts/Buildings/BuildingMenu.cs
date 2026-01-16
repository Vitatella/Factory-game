using NUnit.Framework;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private SelectBuildingButton[] _buttons;

    public void UpdateButtons()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(button.Stats.IsOpened);
        }
    }
}
