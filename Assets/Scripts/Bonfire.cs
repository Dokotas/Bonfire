using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Bonfire : MonoBehaviour
{
    [SerializeField] private float lifeTime, maxHealth;
    [SerializeField] private Sprite[] idle;
    [SerializeField] private Image healthBar;
    
    private Animator _animator;
    public Health health;

    void Start()
    {
        health = new Health(maxHealth, healthBar);

        _animator = GetComponent<Animator>();
        _animator.Run(idle);
        StartCoroutine(Life());
        health.OnDeath += GameManager.Instance.EndGame;
    }

    private IEnumerator Life()
    {
        while (!GameManager.IsPaused)
        {
            health.Damage(1);
            yield return new WaitForSeconds(lifeTime / maxHealth);
        }
    }

}