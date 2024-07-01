using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraChangePos;
    [SerializeField] private Vector3 _playerChangePos;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += _playerChangePos;
            _cam.transform.position += _cameraChangePos;
        }
    }
}
