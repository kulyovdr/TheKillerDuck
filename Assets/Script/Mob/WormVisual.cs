using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class WormVisual : MonoBehaviour
{
    [SerializeField] private Worm _worm;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_RUNNING = "IsRun";
    private const string ATTACK = "Attack";
    private const string IS_DIE = "IsDie";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _worm.OnEnemyAttack += OnEnemyAttack; 
        _worm.OnDie += OnDie;
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _worm.IsRunning());
    }

    private void OnDestroy()
    {
        _worm.OnEnemyAttack -= OnEnemyAttack;
        _worm.OnDie -= OnDie;
    }

    private void OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ATTACK);
    }

    private void OnDie(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
    }

    public void TriggerAttackAnimationTurnOff()
    {
        _worm.PolygonColliderTurnOff();
    }
    public void TriggerAttackAnimationTurnOn()
    {
        _worm.PolygonColliderTurnOn();
    }
}


