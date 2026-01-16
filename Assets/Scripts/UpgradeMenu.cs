using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Upgrade[] _upgrades;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private PlayerStorage _storage;
    [SerializeField] private ItemsCostUI _itemsCostUI;
    public UnityEvent UpgradeApplied;


    private int _currentLevel;

    private void Start()
    {
        _upgrades[_currentLevel].gameObject.SetActive(true);
        _storage.StorageUpdated.AddListener(OnStorageChanged);
    }

    private void OnEnable()
    {
        OnStorageChanged();
    }

    public void OnStorageChanged()
    {
        _upgradeButton.interactable = _storage.IsItemsEnough(_upgrades[_currentLevel].Cost);
        _itemsCostUI.ShowItems(_upgrades[_currentLevel].Cost);
    }

    public void UpgradeButtonPressed()
    {
        _storage.RemoveItems(_upgrades[_currentLevel].Cost);
        _upgrades[_currentLevel].gameObject.SetActive(false);
        _upgrades[_currentLevel].Apply();
        _currentLevel++;
        UpgradeApplied?.Invoke();
        if (_currentLevel <= _upgrades.Length) 
            _upgrades[_currentLevel].gameObject.SetActive(true);
    }
}
