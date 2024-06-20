using Unity.VisualScripting;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;

    [SerializeField] private float _timeBetweenShots;
    private float _currentTimeBetweenShots;

    private Vector3 _difference;
    private float _rotateZ;

    private void Update()
    {
        MoveGunToMouse();
        Bullet_Path_Cooldown();
    }

    private void MoveGunToMouse()
    {
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotateZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotateZ + _offset);
    }

    private void Bullet_Path_Cooldown()
    {
        if (_currentTimeBetweenShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bulletClone = Instantiate(bullet, shotPoint.position, transform.rotation);

                bulletClone.GetComponent<Bullet>().ActivateBullet();
                _currentTimeBetweenShots = _timeBetweenShots;
            }
        }
        else 
        { 
            _currentTimeBetweenShots -= Time.deltaTime; 
        }
    }
}
