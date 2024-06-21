using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _distance;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _whatIsSolid;

    private bool _isMoving = false;


    private void Update()
    {
        HitAndFind();
        if (_isMoving)
        {
            DestroyAfterEndOfLifeTime();
        }
    }

    public float GetLifeTime()
    {
        return _lifeTime;
    }

    public void ActivateBullet()
    {
        _isMoving = true;
    }

    private void HitAndFind()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance, _whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Worm>().TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
        if (_isMoving)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }

    private void DestroyAfterEndOfLifeTime()
    {
        if (_lifeTime < 0)
        {
            Destroy(gameObject);
        }

        _lifeTime -= Time.deltaTime;
    }
}
