using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    private void Update()
    {
        Die();
        FindPlayer();
    }

    private void FindPlayer()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime); // пока влево иди
    }

    private void Die()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }



}
