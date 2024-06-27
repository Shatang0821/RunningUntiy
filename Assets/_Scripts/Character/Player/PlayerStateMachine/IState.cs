public interface IState
{
    /// <summary>
    /// ��ԓ��鏈��
    /// </summary>
    void Enter();
    
    /// <summary>
    /// ��ԏo�鏈��
    /// </summary>
    void Exit();

    /// <summary>
    /// ���W�b�N�X�V����
    /// </summary>
    void LogicUpdate();
    
    /// <summary>
    /// �Œ�Ԋu�̕������Z����
    /// </summary>
    void PhysicUpdate();
}
