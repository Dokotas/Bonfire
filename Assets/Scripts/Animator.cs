using System.Collections;
using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] private float framesPerSecond = 5;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _currentClip;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Run(Sprite[] frames)
    {
        if (_currentClip != null) StopCoroutine(_currentClip);
        _currentClip = StartCoroutine(Animate(frames));
    }

    IEnumerator Animate(Sprite[] frames)
    {
        while (true)
        {
            foreach (var frame in frames)
            {
                if (!GameManager.IsPaused) _spriteRenderer.sprite = frame;
                yield return new WaitForSeconds(1f / framesPerSecond);
            }

            yield return null;
        }
    }
}