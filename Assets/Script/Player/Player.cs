using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] public float _health;

    private float _minMoveSpeed = 0.01f;

    private Rigidbody2D _rigidbody2D;
    private bool _isRun = false;

    Vector2 moveVector;

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

    private void Update()
    {
        moveVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        moveCalculate();
    }
    private void GameInput_OnPLayerAttack(object sender, System.EventArgs e)
    {
       // ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    public void ChangeHealth(float healthValue)
    {
        _health += healthValue;
    }


   /* public bool isRunning()
    {
        return isRun;
    }*/

    public Vector3 GetPlayerPos()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPos;
    }

    private void moveCalculate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + moveVector * (_moveSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(moveVector.x) > _minMoveSpeed || Mathf.Abs(moveVector.y) > _minMoveSpeed)
        {
            _isRun = true;
        }
        else 
        { 
            _isRun = false; 
        }
    }
}

