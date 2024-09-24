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

    // ���\�b�h���͉��ł�OK
    // public�ɂ���K�v������
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_pState.GetState() == MM_PlayerPhaseState.State.Gas)
            return;
        // �ő̂̎����ɐG��ĂȂ������瓮���Ȃ�
        if(_pState.GetState()==MM_PlayerPhaseState.State.Solid)
            if (!isOnWater) return;
        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // 2D�Ȃ̂ŉ��ړ�����
        _velocity = new Vector3(axis.x*_MoveSpeed, 0, 0);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        // �������u�Ԃ�����������
        if (!context.performed) return;
        // �n�ʂɂ��Ȃ��Ȃ璵�ׂȂ�
        if (!isOnGround) return;
        // ���ɐG��Ă����璵�ׂȂ�
        if (isOnWater) return;
        // �C�̂Ȃ璵�ׂȂ�
        if (_pState.GetState() == MM_PlayerPhaseState.State.Gas) return;


        _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);

        print("Jump��������܂���");
    }

    /// <summary>
    /// �C�̂֕ω�
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Gas);

        // �d�͂�0�ɂ���
        nowGravity = 0;
        // ��C��R�𔭐�������
        _rb.drag = 10;

        // ���f�����C�̂̂�ɕς��鏈��
        //
        //

        print("GAS(�C��)�ɂȂ�܂���");
    }

    /// <summary>
    /// �ő̂֕ω�
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Solid);

        // ���f�����ő̂̂�ɕς��鏈��
        //
        //

        print("SOLID(�ő�)�ɂȂ�܂���");
    }
    /// <summary>
    /// �t�̂֕ω�
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // �ő́E�C�́E�X���C������Ȃ�������󂯕t���Ȃ�
        if (_pState.GetState() == MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        // �d�͂�ʏ�ɖ߂�
        nowGravity = _defaultGravity;
        // ��C��R���Ȃ���
        _rb.drag = 0;

        // ���f���𐅂̂�ɕς��鏈��
        //
        //

        print("LIQUID(��)�ɂȂ�܂���");
    }

    /// <summary>
    /// �X���C���֕ω�
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (_pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        _pState.ChangeState(MM_PlayerPhaseState.State.Slime);

        // ���f�����X���C���̂�ɕς��鏈��
        //
        //

        print("SLIME(�X���C��)�ɂȂ�܂���");

    }
}
