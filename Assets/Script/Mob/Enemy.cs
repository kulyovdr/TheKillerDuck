using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State _startState;

    [SerializeField] private bool _isChasingEnemy = false;
    [SerializeField] private float _chasingDistance = 4f;

    [SerializeField] private bool _isAttackEnemy = false;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _damage = 35f;
    private float _nextAttackTime = 0;

    private NavMeshAgent _navMeshAgent;

    private State _currentState;
    private float _chasingSpeed;

    public event EventHandler OnEnemyAttack;

    private enum State
    {
        Idle, Chasing, Death, Attacking
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startState;

        _chasingSpeed = _navMeshAgent.speed;
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
                ChasingTarget();
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

    public void SetDeathState()
    {
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }


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

    private void ChasingTarget()
    {
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
    }
    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Idle;

        if (_isChasingEnemy)
        {
            if (distanceToPlayer <= _chasingDistance)
            {
                newState = State.Chasing;
            }
        }

        if (_isAttackEnemy)
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
                _navMeshAgent.speed = _chasingSpeed;

            }
            else if (newState == State.Attacking)
            {
                _navMeshAgent.ResetPath();
            }
            _currentState = newState;
        }
    }

    public bool IsRunning()
    {
        if (_navMeshAgent.velocity == Vector3.zero)
        {
            return false;
        }
        else { return true; }
    }

    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

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


