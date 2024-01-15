using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CustomJump", fileName = "PlayerState_CustomJump")]
public class PlayerCustomJumpState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float jumpTime = 0.3f;
    private float jumpForce;    // �W�����v��
    

    public override void Enter()
    {
        base.Enter();

        jumpForce = player.customJumpForce;

        // �v���C���[�ɃW�����v�͂�K�p
        player.SetVelocityY(jumpForce);

        // �W�����v���̃p�[�e�B�N���G�t�F�N�g���Đ�
        player.jumpParticle.Play();

        stateTimer = jumpTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        // ���͂Ɋ�Â��ăv���C���[�̌����𐧌�
        player.FlipController(xInput);

        base.LogicUpdate();

        // �㏸���I���A���~���n�߂��痎����Ԃɐ؂�ւ���
        if (rb.velocity.y <= 0)
            stateMachine.SwitchState(typeof(PlayerFallState));

        // �ǂɐڐG���Ă��āA�W�����v���͂�����ꍇ�A�ǃW�����v��Ԃɐ؂�ւ���
        if ((player.HasJumpInputBuffer || Jump) && player.IsWallDetected() && stateTimer < 0)
        {
            //�����𔽓]���Ă���
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
        }


    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
