using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private float _countHearts;
    [SerializeField] private float heal;

    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    [SerializeField] private float _moveSpeed = 10f;

    private Rigidbody2D _rigidbody2D;

    private bool _isRun = false;
    private float _minMoveSpeed = 0.01f;

    private Vector2 _moveInput;
    private Vector2 _moveVelocity;

    public static Player Instance { get; private set; }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPLayerAttack;
    }
    private void Awake()
    {
        Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // --Передвижение--
    private void Update()
    {
        MoveCalculate();
    }

    private void MoveCalculate()
    {
        _moveInput = GameInput.Instance.GetMovementVector();
        _moveVelocity = _moveInput.normalized * _moveSpeed;

        if (Mathf.Abs(_moveInput.x) > _minMoveSpeed || Mathf.Abs(_moveInput.y) > _minMoveSpeed)
        {
            _isRun = true;
        }
        else
        { _isRun = false; }      
    }

    private void statsHearts()
    {
        if (health > _countHearts)
        {
            health = _countHearts;
        }
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                _hearts[i].sprite = _fullHeart;
            }
            else { _hearts[i].sprite = _emptyHeart; }

            if (i < _countHearts)
            {
                _hearts[i].enabled = true;
            }
            else { _hearts[i].enabled = false; }
        }
    }

    private void healHearts()
    {
        health += Time.deltaTime * heal;
    }

    private void FixedUpdate()
    {   
        healHearts();
        statsHearts();
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.fixedDeltaTime);
    }
    public Vector3 GetPlayerPos()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPos;
    }

    public bool isRunning()
    {
        return _isRun;
    }

    public void ChangeHealth(float healthValue)
    {
        health += healthValue;
    }

    private void GameInput_OnPLayerAttack(object sender, System.EventArgs e)
    {
        // ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
}

