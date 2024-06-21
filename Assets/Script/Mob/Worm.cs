using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(NavMeshAgent))]

public class Worm : MonoBehaviour
{
    [SerializeField] private State _startState;
    private State _currentState;

    [SerializeField] private bool _isRun = false;
    [SerializeField] private float _RunDistance = 4f;
    [SerializeField] private float _runSpeed;

    [SerializeField] private bool _isAttack = false;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _damage = 35f;

    [SerializeField] private int _maxHP;
    private int _currentHealth;

    private float _nextAttackTime = 0;
    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private NavMeshAgent _navMeshAgent;

    public event EventHandler OnEnemyAttack;
    //public event EventHandler OnTakeHit;
    public event EventHandler OnDie;

    private enum State
    {
        Idle, Chasing, Death, Attacking
    }

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _currentState = _startState;
        _runSpeed = _navMeshAgent.speed;
    }

    private void Start()
    {
        _currentHealth = _maxHP;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectHandler();
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Chasing:
                RunTarget();
                CheckCurrentState();
                break;

            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                AttackForPlayer();
                break;

            case State.Death:
                break;

            default:
            case State.Idle:
                break;
        }
    }

    //---Бег---
    private void RunTarget()
    {
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
    }
    public bool IsRunning()
    {
        if (_navMeshAgent.velocity == Vector3.zero)
        {
            return false;
        }
        else { return true; }
    }

    //---Боевка---
    private void AttackingTarget()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);

            _nextAttackTime = Time.time + _attackRate;
        }
    }
    public void AttackForPlayer()
    {
        Player player = new Player();
        player.ChangeHealth(-_damage);
    }


    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }
    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    //---Смерть---
    public void SetDeathState()
    {
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        //OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            SetDeathState();

            OnDie?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }


    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Idle;

        if (_isRun)
        {
            if (distanceToPlayer <= _RunDistance)
            {
                newState = State.Chasing;
            }
        }

        if (_isAttack)
        {
            if (distanceToPlayer <= _attackDistance)
            {
                newState = State.Attacking;
            }
        }

        if (newState != _currentState)
        {
            if (newState == State.Chasing)
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _runSpeed;

            }
            else if (newState == State.Attacking)
            {
                _navMeshAgent.ResetPath();
            }
            _currentState = newState;
        }
    }

    //---Поворот относительно игрока---
    private void MovementDirectHandler()
    {
        if (Time.time > _nextCheckDirectionTime)
        {
            if (IsRunning())
            {
                Flip(_lastPosition, transform.position);
            }

            else if (_currentState == State.Attacking)
            {
                Flip(transform.position, Player.Instance.transform.position);
            }

            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }
    private void Flip(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else { transform.rotation = Quaternion.Euler(0, -180, 0); }
    }
}


