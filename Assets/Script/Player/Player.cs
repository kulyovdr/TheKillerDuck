using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _heal;

    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private float _countHearts;
    [SerializeField] private GameObject _deathPanel;


    private Rigidbody2D _rigidbody2D;

    private bool _isRun = false;
    private float _minMoveSpeed = 0.01f;

    private Vector2 _moveInput;
    private Vector2 _moveVelocity;

    public static Player Instance { get; private set; }

    [SerializeField] public Joystick _joystickMove;
    [SerializeField] private Joystick _joystickAttack;
    [SerializeField] public TypeControl _typeControl;

    public enum TypeControl
    {
        PC, Android
    }

    private void WhoIsController()
    {
        if (_typeControl == TypeControl.PC) 
        {
            _joystickMove.gameObject.SetActive(false);
            _joystickAttack.gameObject.SetActive(false);

            _moveInput = GameInput.Instance.GetMovementVectorKeyboard();
            MoveCalculate(_moveInput);

        }
        else if (_typeControl == TypeControl.Android)
        {
            _joystickMove.gameObject.SetActive(true);
            _joystickAttack.gameObject.SetActive(true);

            _moveInput = new Vector2(_joystickMove.Horizontal, _joystickMove.Vertical);
            //_moveInput = GameInput.Instance.GetMovementVectorJoystick(); 
            MoveCalculate(_moveInput);
        }
    }

    private void Awake()
    {
        Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WhoIsController();
        //FlipForJoystick();
        //MoveCalculate();
    }

    private void FixedUpdate()
    {
        HealHearts();
        StatsHearts();
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DroppedHeart"))
        {
            ChangeHealth(1);
            Destroy(other.gameObject);
        }
    }


    private void MoveCalculate(Vector2 _moveInput)
    {
        _moveVelocity = _moveInput.normalized * _moveSpeed;

        if (Mathf.Abs(_moveInput.x) > _minMoveSpeed || Mathf.Abs(_moveInput.y) > _minMoveSpeed) 
        {
            _isRun = true;
        }
        else
        { _isRun = false; }      
    }

    private void StatsHearts()
    {
        if (_health > _countHearts)
        {
            _health = _countHearts;
        }
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(_health))
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

    private void HealHearts()
    {
        _health += Time.deltaTime * _heal;
    }

    public Vector3 GetPlayerPos()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPos;
    }

    public bool IsRunning()
    {
        return _isRun;
    }

    public void ChangeHealth(float healthValue)
    {
        _health += healthValue;

        if (_health <= 0) 
        {
            Destroy(gameObject);
            StatsHearts();
            _deathPanel.SetActive(true);
        }
    }
}

