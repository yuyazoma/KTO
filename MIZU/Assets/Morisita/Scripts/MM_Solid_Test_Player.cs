using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MM_PlayerPhaseState))]
[RequireComponent (typeof(MM_GroundCheck))]

public class MM_Solid_Test_Player: MonoBehaviour
{
    [SerializeField]
    private float _defaultGravity;
    [SerializeField]
    private float nowGravity;
    [SerializeField]
    private float _JumpPower;
    [SerializeField]
    private float _MoveSpeed;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;
    bool isOnWater = false;

    Rigidbody _rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    MM_PlayerPhaseState _pState;
    MM_GroundCheck _groundCheck;

    [SerializeField]
    TextMeshProUGUI Debug_Phasetext;

    private Vector3 _velocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _pState = GetComponent<MM_PlayerPhaseState>();
        _groundCheck = GetComponent<MM_GroundCheck>();

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        nowGravity = _defaultGravity;
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        Debug_Phasetext.text = "Player:" + _pState.GetState();
        //print("Player:" + pState.GetState());
    }

    private void FixedUpdate()
    {
        Gravity();
        GroundCheck();
    }

    void Gravity()
    {
        _rb.AddForce(new Vector3(0, -nowGravity, 0), ForceMode.Force);
    }

    private void GroundCheck()
    {
        isOnGround = _groundCheck.IsGround();
    }

    // メソッド名は何でもOK
    // publicにする必要がある
    public void OnMove(InputAction.CallbackContext context)
    {
        // 固体の時水に触れてなかったら動けない
        if(_pState.GetState()==MM_PlayerPhaseState.State.Solid)
            if (!isOnWater) return;
        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 2Dなので横移動だけ
        _velocity = new Vector3(axis.x*_MoveSpeed, 0, 0);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        // 押した瞬間だけ反応する
        if (!context.performed) return;
        // 地面にいないなら跳べない
        if (!isOnGround) return;
        // 水に触れていたら跳べない
        if (isOnWater) return;
        // 気体なら跳べない
        if (_pState.GetState() == MM_PlayerPhaseState.State.Gas) return;


        _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);

        print("Jumpが押されました");
    }

    /// <summary>
    /// 気体へ変化
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Gas);

        // 重力を0にする
        nowGravity = 0;
        // 空気抵抗を発生させる
        _rb.drag = 10;

        // モデルを気体のやつに変える処理
        //
        //

        print("GAS(気体)になりました");
    }

    /// <summary>
    /// 固体へ変化
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Solid);

        // モデルを固体のやつに変える処理
        //
        //

        print("SOLID(固体)になりました");
    }
    /// <summary>
    /// 液体へ変化
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 固体・気体・スライムじゃなかったら受け付けない
        if (_pState.GetState() == MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        // 重力を通常に戻す
        nowGravity = _defaultGravity;
        // 空気抵抗をなくす
        _rb.drag = 0;

        // モデルを水のやつに変える処理
        //
        //

        print("LIQUID(水)になりました");
    }

    /// <summary>
    /// スライムへ変化
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 水じゃなかったら受け付けない
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Slime);

        // モデルをスライムのやつに変える処理
        //
        //

        print("SLIME(スライム)になりました");

    }
}
