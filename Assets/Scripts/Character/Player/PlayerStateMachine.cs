using System.Collections.Generic;
using UnityEngine;

// �v���C���[�̏�ԃ}�V�����Ǘ�����N���X
public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states; // �v���C���[�̏�Ԃ̃��X�g

    Player player; // �v���C���[�̎Q��

    [SerializeField] PlayerInput input; // �v���C���[�̓���

    // �N�����̏���������
    private void Awake()
    {
        player = GetComponent<Player>(); // �v���C���[�̃R���|�[�l���g���擾

        // ��ԃe�[�u���̏�����
        stateTable = new Dictionary<System.Type, IState>(states.Length);

        // �e��Ԃ����������A��ԃe�[�u���ɒǉ�
        foreach (PlayerState state in states)
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
}
