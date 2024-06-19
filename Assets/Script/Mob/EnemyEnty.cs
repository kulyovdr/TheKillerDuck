using UnityEngine;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyEntry : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDie;

    [SerializeField] private int _maxHP;
    private int _currentHealth;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private Enemy _enemyAI;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<Enemy>();
    }

    private void Start()
    {
        _currentHealth = _maxHP;
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }


    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDie?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);          
        }
    }
}
