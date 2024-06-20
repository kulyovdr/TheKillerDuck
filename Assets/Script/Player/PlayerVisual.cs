using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_RUN = "IsRun";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(IS_RUN, Player.Instance.isRunning());
        MovePos_ForMouse_ForKeyboard();
    }

    private void MovePos_ForMouse_ForKeyboard()
    {
        Vector3 mousePOs = GameInput.Instance.GetMousePosition();
        Vector3 playerPOs = Player.Instance.GetPlayerPos();

        if (mousePOs.x < playerPOs.x) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }
        else  
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
