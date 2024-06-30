using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField] private Room roomPrefab;

    [SerializeField] private int count;

    private IEnumerator Generate()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        var infos = graph.Generate1(count);

        foreach (var pos in infos.Keys)
        {
            var room = Instantiate(roomPrefab, new Vector3(pos.x * 64f, pos.y * 36f), Quaternion.identity);
            room.Setup(infos[pos]);

            yield return 0;
        }
    }

    void Start()
    {
        StartCoroutine(Generate());
    }

}
