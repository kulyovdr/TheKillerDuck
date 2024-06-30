using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private Vector3 cameraChangePos;
    [SerializeField] private Vector3 playerChangePos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
