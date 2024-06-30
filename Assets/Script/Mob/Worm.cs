using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(NavMeshAgent))]

public class Worm : MonoBehaviour
{
    [SerializeField] private State _startState;
    [SerializeField] private State _currentState;

    [SerializeField] private float _RunDistance;
    [SerializeField] private float _runSpeed;

    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _damage;

    [SerializeField] private GameObject _droppedHeart;
    [SerializeField] private GameObject _sword;

    [SerializeField] private int _maxHP;
    private int _currentHealth;

    private float _nextAttackTime = 1f;
    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    private PolygonCollider2D _polygonCollider2D;
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
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _currentState = _startState;
        _runSpeed = _navMeshAgent.speed;

        _lastPosition = transform.position;
    }

    private void Start()
    {
        _currentHealth = _maxHP;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectHandler();
        DetectDeath();
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
                break;

            case State.Death:
                break;

            default:
            case State.Idle:
                CheckCurrentState();
                break;
        }
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer;
        if (Player.Instance != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        }
        else
        {
            distanceToPlayer = _RunDistance + 1f;
        }
        State newState = State.Idle;

        if (distanceToPlayer <= _RunDistance)
        {
            newState = State.Chasing;
        }

        if (distanceToPlayer <= _attackDistance)
        {
            newState = State.Attacking;
        }

        if (newState != _currentState)
        { 
            _currentState = newState;
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
        if (Time.time > _nextAttackTime && Player.Instance != null)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            Player.Instance.ChangeHealth(-_damage);

            _nextAttackTime = Time.time + _attackRate;
        }
    }

    public void PolygonColliderTurnOff()
    {
        //_polygonCollider2D.enabled = false;
        _sword.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void PolygonColliderTurnOn()
    {
        //_polygonCollider2D.enabled = true;
        _sword.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    //---Смерть---
    public void SetDeathState()
    {
        //_polygonCollider2D.enabled = false;
        _sword.GetComponent<CapsuleCollider2D>().enabled = false;
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        //OnTakeHit?.Invoke(this, EventArgs.Empty);
    }
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            SetDeathState();
            Instantiate(_droppedHeart, transform.position, transform.rotation);

            OnDie?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
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


