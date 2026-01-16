using UnityEngine;
using UnityEngine.Events;

public class BaseDestroyHandler : MonoBehaviour
{
    public UnityEvent BaseDestroyed;
    
    public void OnBaseDestroyed()
    {
        BaseDestroyed?.Invoke();
    }
}
