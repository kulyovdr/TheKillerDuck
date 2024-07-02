using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public class NodeInfo
    {
        public Vector2Int Pos;
        public List<Vector2Int> Dirs;

        public NodeInfo(Vector2Int pos, List<Vector2Int> dirs)
        {
            Pos = pos;
            Dirs = dirs;
        }
    }

    private readonly Vector2Int[] roomDirections = new Vector2Int[]
{
            new (0, 1),
            new (1, 0),
            new (0, -1),
            new (-1, 0)
};

    private readonly Vector2Int[] roomProbabilities = new Vector2Int[]
    {
            new (0, 25),
            new (25, 50),
            new (50, 75),
            new (75, 100)
    };


    /// <summary>
    /// Версия с одним проходом
    /// </summary>
    /// <param name="count">Кол-во вершин</param>
    /// <returns>Словрь с позициями вершин и направлениями связей</returns>
    public Dictionary<Vector2Int, HashSet<Vector2Int>> Generate(int count)
    {
        var node1 = new Vector2Int(0, 0);

        var nodes = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        nodes.Add(node1, new HashSet<Vector2Int>());

        for (int i = 0; i < count - 1; i++)
        {
            bool next = false;

            while (!next)
            {
                var node2 = node1 + roomDirections[GetIndexWithProb(roomProbabilities)];

                if (!nodes.ContainsKey(node2))
                {
                    next = true;

                    nodes.Add(node2, new HashSet<Vector2Int>());
                }

                nodes[node1].Add(node2 - node1);
                nodes[node2].Add(node1 - node2);

                node1 = node2;
            }
        }

        return nodes;
    }


    private int GetIndexWithProb(Vector2Int[] probs)
    {
        int value = Random.Range(0, 101);

        for (int i = 0; i < probs.Length; i++)
        {
            if (value >= probs[i].x && value < probs[i].y)
            {
                return i;
            }
        }

        return 0;
    }
}
