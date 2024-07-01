using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Openings _doors;

    [SerializeField] private Openings _walls;

    [SerializeField] private GameObject _linkRight;
    [SerializeField] private GameObject _linkTop;

    public void Setup(HashSet<Vector2Int> config)
    {
        foreach (var wall in _walls.GetOpenings())
        {
            bool contains = config.Contains(wall.Pos);
            wall.GameObject.SetActive(!contains);
            if (wall.Pos == Vector2Int.right)
            {
                _linkRight.SetActive(contains);
            }
            else if (wall.Pos == Vector2Int.up)
            {
                _linkTop.SetActive(contains);
            }
        }
    }
}
