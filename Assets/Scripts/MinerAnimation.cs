using Unity.VisualScripting;
using UnityEngine;

public class MinerAnimation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private bool _isPlaying;
    private Coroutine _rotation;


    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + _rotationSpeed * Time.deltaTime);
    }
}
