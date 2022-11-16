using System.Collections;
using UnityEngine;

public class Enemy : Walker
{
    [SerializeField] private float viewRadius, findNewDestinationTime;
    private bool _isWaiting;
    private Transform _player, _bonfire;


    private void Start()
    {
        base.Start();
        Health.OnDeath += () => Destroy(gameObject);
        _isWaiting = true;
        StartCoroutine(LifeLoop());
        _player = GameManager.Player.transform;
        _bonfire = GameManager.Bonfire.transform;
    }

    void Update()
    {
        if (Vector3.Distance(_player.position, transform.position) <= viewRadius)
        {
            _isWaiting = false;
            _destination = _player.position;
        }
        
        else
        {
            _isWaiting = true;
        }

        Move();
    }

    IEnumerator LifeLoop()
    {
        while (true)
        {
            if(_isWaiting)
                _destination = FindTarget();
            
            yield return new WaitForSeconds(findNewDestinationTime);
        }
    }

    private Vector3 FindTarget()
    {
        Vector3 moveVector = Random.insideUnitCircle * viewRadius;
        Vector3 result;
        do
        {
            result = new Vector3(transform.position.x + moveVector.x, transform.position.y + moveVector.y, 0);
        } while (Vector3.Distance(result, _bonfire.position) <= viewRadius);

        return result;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        var collision = col.collider;
        if (collision.TryGetComponent(out Player player))
        {
            player.Health.Damage(damage);
        }
    }
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.TryGetComponent(out Player player))
        {
            player.Health.Damage(damage);
        }
    }
}