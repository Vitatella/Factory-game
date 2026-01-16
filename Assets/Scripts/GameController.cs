using UnityEngine;

public class GameController : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}
