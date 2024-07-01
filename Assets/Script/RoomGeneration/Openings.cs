using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Проемы
/// </summary>
public class Openings : MonoBehaviour
{
    /// <summary>
    /// Класса проема
    /// </summary>
    [System.Serializable]
    public class Opening
    {
        /// <summary>
        /// Расположение проема относительно центра
        /// </summary>
        public Vector2Int Pos;

        /// <summary>
        /// Объект проемы (стена, дверь, шторы и т.д.)
        /// </summary>
        public GameObject GameObject;
    }

    [SerializeField] private Opening _top;

    [SerializeField] private Opening _right;

    [SerializeField] private Opening _bottom;

    [SerializeField] private Opening _left;

    private List<Opening> _list;

    public List<Opening> GetOpenings()
    {
        if (_list == null)
        {
            _list = new List<Opening>()
            {
                _top,
                _right,
                _bottom,
                _left
            };
        }

        return _list;
    }
}
