using System;
using UnityEngine;
using UnityEngine.UI;

public class Health
{
    private float _healthValue, _maxHealth;
    private Image _healthBar;
    public Action OnDeath;

    public Health(float maxHealth, Image healthBar)
    {
        _maxHealth = maxHealth;
        _healthBar = healthBar;
        Heal(maxHealth);
    }

    public void Damage(float value)
    {
        if (!GameManager.IsPaused)
        {
            _healthBar.transform.parent.gameObject.SetActive(true);
            if (_healthValue <= 0)
                Death();

            _healthValue -= value;
            _healthBar.fillAmount = _healthValue / _maxHealth;
        }
    }

    public void Heal(float value)
    {
        if (!GameManager.IsPaused)
        {
            _healthValue = Mathf.Min(_maxHealth, _healthValue + value);
            _healthBar.fillAmount = _healthValue / _maxHealth;
            
            if (_healthValue >= _maxHealth)
                _healthBar.transform.parent.gameObject.SetActive(false);
            
        }
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }
}