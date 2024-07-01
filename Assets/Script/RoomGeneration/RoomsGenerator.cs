using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private GameObject _worm;

    [SerializeField] private int _count;

    private IEnumerator Generate()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        var infos = graph.Generate1(_count);

        foreach (var pos in infos.Keys)
        {
            var room = Instantiate(_roomPrefab, new Vector3(pos.x * 64f, pos.y * 36f), Quaternion.identity);
            for (int i = 0; i < Random.Range(0, 4); i++)
            {
                Instantiate(_worm, new Vector3(pos.x * 64f, pos.y * 36f) + new Vector3(Random.Range(-18, 19), Random.Range(-10, 11)), Quaternion.identity);
            }
            room.Setup(infos[pos]);

            yield return 0;
        }
    }

    void Start()
    {
        StartCoroutine(Generate());
    }
}
