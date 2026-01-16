using UnityEngine;

public class PlayerShootingModeRotation : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector2 direction = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle+180));
    }
}
