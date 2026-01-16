using UnityEngine;

public class BuildingsInitializer : MonoBehaviour
{
    [SerializeField] private BuildingStats[] _buildings;

    void Awake()
    {
        foreach (var building in _buildings)
        {
            building.Initialize();
        }
    }

}
