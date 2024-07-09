using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MM_PlayerPhaseState))]

public class MM_Test_Player: MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _JumpHeight;
    [SerializeField]
    private float _MoveSpeed;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;

    Rigidbody _rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    [SerializeField]
    MM_PlayerPhaseState pState;

    private Vector3 _velocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        pState = GetComponent<MM_PlayerPhaseState>();

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        pState.ChangeState(MM_PlayerPhaseState.State.Liquid);
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        print("Player:" + pState.GetState());
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        _rb.AddForce(new Vector3(0, -_gravity, 0), ForceMode.Force);
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
        _velocity = new Vector3(axis.x*_MoveSpeed, 0, 0);
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


        _rb.AddForce(new Vector3(0, _JumpHeight, 0), ForceMode.VelocityChange);

        print(callOnJumpCount++ + ":Jumpが押されました");
    }

    /// <summary>
    /// 気体へ変化
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Gas);
        print("GAS(気体)になりました");
    }

    /// <summary>
    /// 固体へ変化
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Solid);

        print("SOLID(固体)になりました");
    }
    /// <summary>
    /// 液体へ変化
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 固体・気体・スライムじゃなかったら受け付けない
        if (pState.GetState() == MM_PlayerPhaseState.State.Liquid) return;



        pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        print("LIQUID(水)になりました");
    }

    /// <summary>
    /// スライムへ変化
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Slime);

        print("SLIME(スライム)になりました");

    }
}
