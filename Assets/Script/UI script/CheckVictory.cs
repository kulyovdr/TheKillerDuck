using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVictory : MonoBehaviour
{
    [SerializeField] private GameObject _victoryPanel;

    public int wormsCount;

    public static CheckVictory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        wormsCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (wormsCount <= 0)
        {
            _victoryPanel.SetActive(true);
        }
    }
}
