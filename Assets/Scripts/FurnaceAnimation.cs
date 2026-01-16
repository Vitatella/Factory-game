using System.Collections;
using UnityEngine;

public class FurnaceAnimation : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite[] _workingSprites;
    [SerializeField] private float _frameTime;
    [SerializeField] private SpriteRenderer _renderer;
    private Coroutine _animationRoutine;


    public void Play()
    {
        _animationRoutine = StartCoroutine(Animation());
    }

    public void Stop()
    {
        StopCoroutine(_animationRoutine);
        _renderer.sprite = _defaultSprite;
    }

    private IEnumerator Animation()
    {
        WaitForSeconds wait = new WaitForSeconds(_frameTime);
        int i = 0;
        while (true)
        {
            _renderer.sprite = _workingSprites[i];
            i = i < _workingSprites.Length - 1 ? i + 1 : 0;
            yield return wait;
        }
    }
}
