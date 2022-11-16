using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Walker : MonoBehaviour
{
    public Health Health;
    
    [SerializeField] private float speed, maxHealth;
    [SerializeField] protected float hitRadius, damage;
    [SerializeField] private Image healthBar;
    [SerializeField] private Sprite[] idle, run;
    
    protected Vector3 _destination;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private bool _isRunning;
    
    public void Start()
    {
        Health = new Health(maxHealth, healthBar);
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _destination = transform.position;
        _animator = GetComponent<Animator>();
        _animator.Run(idle);
    }

    protected List<GameObject> CheckCollisions(float radius)
    {
        List<GameObject> result = new List<GameObject>(); 
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.CircleCastNonAlloc(transform.position, radius, Vector2.one, hits);
        foreach (var hit in hits)
        {
            if(hit && hit.collider != _collider)
                result.Add(hit.collider.gameObject);
        }

        return result;
    }

    protected void Move()
    {
        if (transform.position != _destination && !GameManager.IsPaused &&
            Vector3.Distance(Vector3.zero, _destination) < GameManager.WorldSize)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, speed*Time.deltaTime);
            _spriteRenderer.flipX = _destination.x < transform.position.x;
            if (!_isRunning)
            {
                _isRunning = true;
                _animator.Run(run);
            }
        }

        else if (_isRunning)
        {
            _isRunning = false;
            _animator.Run(idle);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _destination = transform.position;
    }

}