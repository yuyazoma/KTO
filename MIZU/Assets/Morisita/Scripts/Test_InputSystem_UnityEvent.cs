using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Test_InputSystem_UnityEvent : MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _JumpHeight;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;

    Rigidbody rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    PlayerPhaseState.State pState;

    private Vector3 _velocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        pState = GetComponent<PlayerPhaseState.State>();

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        pState = PlayerPhaseState.State.Liquid;
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        rb.AddForce(new Vector3(0, -_gravity, 0), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }


    // メソッド名は何でもOK
    // publicにする必要がある
    public void OnMove(InputAction.CallbackContext context)
    {
        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 2Dなので横移動だけ
        _velocity = new Vector3(axis.x, 0, 0);
    }
    int callOnJumpCount = 0;
    public void OnJump(InputAction.CallbackContext context)
    {
        // 押した瞬間だけ反応する
        if (!context.performed) return;

        // こっちでもいいけどif文の中になって見づらくなる
        //if (context.performed)

        // 地面にいないなら跳べない
        if (!isOnGround) return;


        rb.AddForce(new Vector3(0, _JumpHeight, 0), ForceMode.VelocityChange);

        print(callOnJumpCount++ + ":Jumpが押されました");
    }

    /// <summary>
    /// 気体へ変化
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (pState != PlayerPhaseState.State.Liquid) return;
        print("GAS");
    }

    /// <summary>
    /// 固体へ変化
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (pState != PlayerPhaseState.State.Liquid) return;

        print("SOLID");
    }
    /// <summary>
    /// 液体へ変化
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 固体・気体・スライムじゃなかったら受け付けない
        if (pState != PlayerPhaseState.State.Solid) return;
        if (pState != PlayerPhaseState.State.Gas) return;
        if (pState != PlayerPhaseState.State.Slime) return;


        print("LIQUID");
    }

    /// <summary>
    /// スライムへ変化
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // 水じゃなかったら受け付けない
        if (pState != PlayerPhaseState.State.Liquid) return;

        print("SLIME");

    }
}
