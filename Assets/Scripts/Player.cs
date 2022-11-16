using System;
using TMPro;
using UnityEngine;

public class Player : Walker
{
    private Camera _camera;
    private float _fuelAmount;

    [SerializeField] private float bonfireHealing;
    [SerializeField] private TextMeshProUGUI fuelScore;

    private void Start()
    {
        base.Start();
        _camera = Camera.main;
        fuelScore.text = "Fuel: " + _fuelAmount;
        Health.OnDeath += GameManager.Instance.EndGame;
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && !GameManager.IsPaused)
        {
            Touch touch = Input.GetTouch(0);
            var touchPos = _camera.ScreenToWorldPoint(touch.position);
            Destination = new Vector3(touchPos.x, touchPos.y, 0);
        }
#else
        var touchPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
            _destination = new Vector3(touchPos.x, touchPos.y, 0);
#endif

        Move();

        // var collisions = CheckCollisions(hitRadius);
        // foreach (var collision in collisions)
        // {
        //     if (collision.TryGetComponent(out Fuel fuel))
        //     {
        //         _fuelAmount += fuel.PickUp();
        //         fuelScore.text = "Fuel: " + _fuelAmount;
        //     }
        //
        //     if (collision.TryGetComponent(out Bonfire bonfire))
        //     {
        //         bonfire.AddFuel(_fuelAmount);
        //         health.Heal(bonfireHealing);
        //         _fuelAmount = 0;
        //         fuelScore.text = "Fuel: " + _fuelAmount;
        //     }
        //     
        //     if (collision.TryGetComponent(out Enemy enemy))
        //     {
        //         enemy.health.Damage(damage);
        //     }
        // }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        _destination = transform.position;
        var collision = col.collider;
        if (collision.TryGetComponent(out Fuel fuel))
        {
            _fuelAmount += fuel.PickUp();
            fuelScore.text = "Fuel: " + _fuelAmount;
        }

        if (collision.TryGetComponent(out Bonfire bonfire))
        {
            bonfire.health.Heal(_fuelAmount);
            _fuelAmount = 0;
            fuelScore.text = "Fuel: " + _fuelAmount;
        }
    }
    
    private void OnCollisionStay2D(Collision2D col)
    {
        var collision = col.collider;
        
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.Damage(damage);
        }
        
        if (collision.TryGetComponent(out Bonfire bonfire))
        {
            Health.Heal(bonfireHealing);
        }
    }
}