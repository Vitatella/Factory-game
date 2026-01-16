using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    private Vector2 _moveDirection;
    public Vector2 MoveDirection { get => _moveDirection; set => _moveDirection = value; }

    void Update()
    {
        if (_moveDirection == Vector2.zero) return;
        _rigidbody.AddForce(_moveDirection * _speed * Time.deltaTime);
    }
}
