using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    //private SpriteRenderer _spriteRenderer;

    private const string IS_RUN = "IsRun";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(IS_RUN, Player.Instance.IsRunning());
        MovePos_ForMouse_ForKeyboard();
    }

    private void joysticAttackFlip()
    {
        Player player = Player.Instance;
      //  if ( player._typeControl == player.TypeControl.Android)      
    }

    private void MovePos_ForMouse_ForKeyboard()
    {
        Vector3 mousePOs = GameInput.Instance.GetMousePosition();
       // Vector3 joysticPOs = GameInput.Instance.GetJoystickPosition();

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

        Player player = Player.Instance;
        if (player.joystickMove.Horizontal < 0 || player.joystickAttack.Horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (player.joystickMove.Horizontal > 0 || player.joystickAttack.Horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
