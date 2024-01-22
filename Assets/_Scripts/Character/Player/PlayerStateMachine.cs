using System.Collections.Generic;
using UnityEngine;

// �v���C���[�̏�ԃ}�V�����Ǘ�����N���X
public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states; // �v���C���[�̏�Ԃ̃��X�g

    Player player; // �v���C���[�̎Q��

    [SerializeField] PlayerInput input; // �v���C���[�̓���

    //GUI�̕\���i�f�o�b�O�p�j
    //void OnGUI()
    //{
    //    Rect rect = new Rect(200, 150, 200, 200);
    //    string message = currentState.ToString();
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 50;
    //    style.fontStyle = FontStyle.Bold;
    //    GUI.Label(rect, message, style);
    //}

    // �N�����̏���������
    private void Awake()
    {
        player = GetComponent<Player>(); // �v���C���[�̃R���|�[�l���g���擾

        // ��ԃe�[�u���̏�����
        stateTable = new Dictionary<System.Type, IState>(states.Length);

        // �e��Ԃ����������A��ԃe�[�u���ɒǉ�
        foreach (var state in states)
        {
            state.Initialize(player, this, input); // �e��Ԃ̏�����
            stateTable.Add(state.GetType(), state); // ��ԃe�[�u���ɏ�Ԃ�ǉ�
        }
    }

    // �J�n���̏���
    private void Start()
    {
        // ������ԁi�A�C�h����ԁj�ɐ؂�ւ�
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    // �L�������̏���
    private void OnEnable()
    {
        // ������ԁi�A�C�h����ԁj�ɐ؂�ւ�
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    /// <summary>
    /// �J�X�^���W�����v�X�e�[�^�X�ɋ����I�ɕύX
    /// </summary>
    public void ForceJumpStateChange(float force)
    {
        EventCenter.TriggerEvent(StateEvents.SetCustomJumpForce, force);
        SwitchState(stateTable[typeof(PlayerCustomJumpState)]);
    }
}
