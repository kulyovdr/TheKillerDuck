using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SceletonVisual : MonoBehaviour
{
    [SerializeField] private Enemy _enemyAI;
    [SerializeField] private EnemyEntry _enemyEntry;
    private Animator _animator;

    private const string IS_RUNNING = "IsRun";
    private const string ATTACK = "Attack";
    private const string IS_DIE = "IsDie";

    SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack; //подписались на событие
        _enemyEntry.OnDie += _enemyEntry_OnDie;
    }
    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ATTACK);
    }

    private void _enemyEntry_OnDie(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
    }


    private void OnDestroy() // отписались
    {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
    }


    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _enemyAI.IsRunning());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        _enemyEntry.PolygonColliderTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        _enemyEntry.PolygonColliderTurnOn();
    }
}


