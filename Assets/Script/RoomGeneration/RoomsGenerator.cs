using System.Collections;
using UnityEngine;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private GameObject _worm;

    [SerializeField] private int _count;

    private IEnumerator Generate()
    {
        Graph graph = gameObject.AddComponent<Graph>();
        var infos = graph.Generate(_count);

        foreach (var pos in infos.Keys)
        {
            var room = Instantiate(_roomPrefab, new Vector3(pos.x * 64f, pos.y * 36f), Quaternion.identity);

            int rand = Random.Range(0, 4);
            for (int i = 0; i < rand; i++)
            {
                Instantiate(_worm, new Vector3(pos.x * 64f, pos.y * 36f) + new Vector3(Random.Range(-18, 19), Random.Range(-10, 11)), Quaternion.identity);
            }
            room.Setup(infos[pos]);

            CheckVictory.Instance.wormsCount += rand;

            yield return 0;
        }
    }

    private void Start()
    {
        StartCoroutine(Generate());
    }
}
