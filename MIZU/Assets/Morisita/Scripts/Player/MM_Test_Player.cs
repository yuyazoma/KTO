using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MM_PlayerPhaseState))]
[RequireComponent(typeof(MM_GroundCheck))]

public class MM_Test_Player : MonoBehaviour
{
    [SerializeField]
    [Header("デバッグモード")]
    bool IS_DEBUGMODE = false;
    [SerializeField]
    private float _defaultGravity;
    [SerializeField]
    private float nowGravity;
    [SerializeField]
    private float _JumpPower;
    [SerializeField]
    private float _MovePower;
    [SerializeField]
    private float _LimitSpeed;
    [SerializeField, Header("慣性力,-1~10")]
    private float _InertiaPower;
    [SerializeField]
    private float _NowXSpeed;
    [SerializeField]
    private float _NowYSpeed;
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

    private KK_PlayerModelSwitcher _modelSwitcher;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _pState = GetComponent<MM_PlayerPhaseState>();
        _groundCheck = GetComponent<MM_GroundCheck>();
        _modelSwitcher = GetComponent<KK_PlayerModelSwitcher>(); // PlayerModelSwitcher コンポーネントを取得

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        nowGravity = _defaultGravity;

        _InertiaPower = Mathf.Clamp(_InertiaPower, -1, 10);

    }

    private void Update()
    {
        if (Debug_Phasetext != null)
            Debug_Phasetext.text = "Player:" + _pState.GetState();
        PlayerStateUpdateFunc();
    }

    private void FixedUpdate()
    {
        Gravity();
        GroundCheck();
        Move();
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Death();
        }
    }

    void Gravity()
    {
        _rb.AddForce(new Vector3(0, -nowGravity, 0), ForceMode.Acceleration);
    }

    void Move()
    {
        var nowXSpeed = Mathf.Sqrt(Mathf.Pow(_rb.velocity.x, 2));
        var nowYSpeed = Mathf.Sqrt(Mathf.Pow(_rb.velocity.y, 2));
        _NowXSpeed = nowXSpeed;
        _NowYSpeed = nowYSpeed;

        // それ以外の時の横移動
        if (_pState.GetState() != MM_PlayerPhaseState.State.Gas)
        {
            if (_velocity.x != 0)
                _rb.AddForce(_velocity, ForceMode.Acceleration);
            else
                _rb.AddForce(new Vector3(-_rb.velocity.x * _InertiaPower, _rb.velocity.y, _rb.velocity.z), ForceMode.Acceleration);
        }
        // ガスの時の縦移動
        else
        {
            if (_velocity.y != 0)
                _rb.AddForce(_velocity, ForceMode.Acceleration);
            else
                _rb.AddForce(new Vector3(_rb.velocity.x, -_rb.velocity.y * _InertiaPower, _rb.velocity.z), ForceMode.Acceleration);
        }

        if (nowXSpeed > _LimitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x / (nowXSpeed / _LimitSpeed), _rb.velocity.y, _rb.velocity.z);
            //print("LimitedXSpeed");
        }
        if (nowYSpeed > _LimitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y / (nowYSpeed / _LimitSpeed), _rb.velocity.z);
            //print("LimitedYSpeed");
        }

    }

    private void GroundCheck()
    {
        isOnGround = _groundCheck.IsGround();
        isOnWater = _groundCheck.IsPuddle();
    }

    private void PlayerStateUpdateFunc()
    {
        switch (_pState.GetState())
        {
            case MM_PlayerPhaseState.State.Gas: PlayerGasStateUpdateFunc(); break;
            case MM_PlayerPhaseState.State.Solid: PlayerSolidStateUpdateFunc(); break;
            case MM_PlayerPhaseState.State.Liquid: PlayerLiquidStateUpdateFunc(); break;
            case MM_PlayerPhaseState.State.Slime: PlayerSlimeStateUpdateFunc(); break;
            default: Debug.LogError($"エラー、プレイヤーのステートが{_pState.GetState()}になっています"); break;
        }
    }

    private void PlayerGasStateUpdateFunc()
    {

    }
    private void PlayerSolidStateUpdateFunc()
    {

    }
    private void PlayerLiquidStateUpdateFunc()
    {
        StartCoroutine(IsPuddleCollisionDeadCount());
    }
    private void PlayerSlimeStateUpdateFunc()
    {

    }


    private void Death()
    {
        this.gameObject.SetActive(false);
    }
    // メソッド名は何でもOK
    // publicにする必要がある
    public void OnMove(InputAction.CallbackContext context)
    {
        // 気体なら横移動はできない
        if (_pState.GetState() == MM_PlayerPhaseState.State.Gas) return;
        // 固体の時水に触れてなかったら動けない
        if (_pState.GetState() == MM_PlayerPhaseState.State.Solid)
            if (!isOnWater) return;
        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 2Dなので横移動だけ
        _velocity = new Vector3(axis.x * _MovePower, 0, 0);

    }
    public void OnGasMove(InputAction.CallbackContext context)
    {
        // 気体でなければ縦移動はできない
        if (_pState.GetState() != MM_PlayerPhaseState.State.Gas)
            return;

        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 気体の時は縦移動だけ
        _velocity = new Vector3(0, axis.y * _MovePower, 0);
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

        _velocity = new Vector3(_velocity.x, 0, 0);

        _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);

        print("Jumpが押されました");
    }


    // 水に触れたら死亡までのカウントを開始
    private IEnumerator IsPuddleCollisionDeadCount()
    {
        float contactTime = 0f;
        float destroyTime = 2f;

        while (isOnWater)
        {
            contactTime += Time.deltaTime;
            yield return null;
            if (contactTime >= destroyTime)
            {
                Death();
            }
            // print($"{nameof(contactTime)}:{contactTime}");
        }
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

        print("GAS(気体)になりました");

        if (IS_DEBUGMODE)
            return;
        // モデルを気体のやつに変える処理
        _modelSwitcher.SwitchToModel(_modelSwitcher.gasModel);
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


        _velocity = Vector3.zero;
        //_rb.angularVelocity = Vector3.zero;

        print("SOLID(固体)になりました");

        if (IS_DEBUGMODE)
            return;
        // モデルを固体のやつに変える処理
        _modelSwitcher.SwitchToModel(_modelSwitcher.solidModel);
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

        print("LIQUID(水)になりました");

        if (IS_DEBUGMODE)
            return;
        // モデルを水のやつに変える処理
        _modelSwitcher.SwitchToModel(_modelSwitcher.liquidModel);
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

        print("SLIME(スライム)になりました");

        if (IS_DEBUGMODE)
            return;
        // モデルをスライムのやつに変える処理
        _modelSwitcher.SwitchToModel(_modelSwitcher.slimeModel);
        //

        print("SLIME(スライム)になりました");

    }
}
