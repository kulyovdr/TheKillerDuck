using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;

    [SerializeField] private float _startBtwShots;
    private float _timeBtwShots;

    private Vector3 _difference;
    private float _rotateZ;

    private void Update()
    {
        MoveQunForMouse();
        Bullet_Path_CallDown();
    }

    private void MoveQunForMouse()
    {
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotateZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotateZ + _offset);
    }

    private void Bullet_Path_CallDown()
    {
        if (_timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                _timeBtwShots = _startBtwShots;
            }
        }
        else 
        { 
            _timeBtwShots -= Time.deltaTime; 
        }
    }
}
