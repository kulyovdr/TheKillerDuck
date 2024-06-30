using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private GameObject grass;

    private enum Direction
    {
        Top, Bottom, Left, Right, None
    }

    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private readonly float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        if (!spawned)
        {
            switch (direction)
            {
                case Direction.Top:
                    rand = Random.Range(0, variants.topRooms.Length);
                    Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
                    Instantiate(grass, transform.position, variants.topRooms[rand].transform.rotation);
                    break;
                case Direction.Bottom:
                    rand = Random.Range(0, variants.bottomRooms.Length);
                    Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
                    Instantiate(grass, transform.position, variants.bottomRooms[rand].transform.rotation);
                    break;
                case Direction.Left:
                    rand = Random.Range(0, variants.leftRooms.Length);
                    Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
                    Instantiate(grass, transform.position, variants.leftRooms[rand].transform.rotation);
                    break;
                case Direction.Right:
                    rand = Random.Range(0, variants.rightRooms.Length);
                    Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
                    Instantiate(grass, transform.position, variants.rightRooms[rand].transform.rotation);
                    break;
                default:
                    break;
            }
            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}
