﻿using System.Collections;
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


    /// <summary>
    /// Версия с одним проходом
    /// </summary>
    /// <param name="count">Кол-во вершин</param>
    /// <returns>Словрь с позициями вершин и направлениями связей</returns>
    public Dictionary<Vector2Int, HashSet<Vector2Int>> Generate1(int count)
    {
        var n1 = new Vector2Int(0, 0);

        var nodes = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        nodes.Add(n1, new HashSet<Vector2Int>());

        var dxdy = new Vector2Int[]
        {
            new (0, 1),
            new (1, 0),
            new (0, -1),
            new (-1, 0)
        };

        var probs = new Vector2Int[]
        {
            new (0, 25),
            new (25, 50),
            new (50, 75),
            new (75, 100)
        };

        for (int i = 0; i < count - 1; i++)
        {
            bool next = false;

            while (!next)
            {
                var n2 = n1 + dxdy[GetIndexWithProb(probs)];

                if (!nodes.ContainsKey(n2))
                {
                    next = true;

                    nodes.Add(n2, new HashSet<Vector2Int>());
                }

                nodes[n1].Add(n2 - n1);
                nodes[n2].Add(n1 - n2);

                n1 = n2;
            }
        }

        return nodes;
    }


    /// <summary>
    /// Версия с двумя проходами
    /// </summary>
    /// <param name="count">Кол-во вершин</param>
    /// <returns>Информация о нодах</returns>
    public List<NodeInfo> Generate2(int count)
    {
        var nodes = CreateGraph(count);
        var start = nodes.Keys.First();

        var visited = new HashSet<Vector2Int>();
        var queue = new Queue<Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);

        var result = new List<NodeInfo>();

        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            var dirs = new List<Vector2Int>();

            foreach (var item in nodes[node])
            {
                if (!visited.Contains(item))
                {
                    queue.Enqueue(item);
                    visited.Add(item);
                }

                dirs.Add(item - node);
            }

            result.Add(new NodeInfo(node, dirs));
        }

        return result;
    }

    private Dictionary<Vector2Int, HashSet<Vector2Int>> CreateGraph(int count)
    {
        var n1 = new Vector2Int(0, 0);

        var nodes = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        nodes.Add(n1, new HashSet<Vector2Int>());

        var dxdy = new Vector2Int[]
        {
            new (0, 1),
            new (1, 0),
            new (0, -1),
            new (-1, 0)
        };

        var probs = new Vector2Int[]
        {
            new (0, 25),
            new (25, 50),
            new (50, 75),
            new (75, 100)
        };

        for (int i = 0; i < count - 1; i++)
        {
            bool next = false;

            while (!next)
            {
                var n2 = n1 + dxdy[GetIndexWithProb(probs)];

                if (!nodes.ContainsKey(n2))
                {
                    next = true;

                    nodes.Add(n2, new HashSet<Vector2Int>());
                }

                nodes[n1].Add(n2);
                nodes[n2].Add(n1);

                n1 = n2;
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
