using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CustomJump", fileName = "PlayerState_CustomJump")]
public class PlayerCustomJumpState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float jumpTime = 0.3f; //�W�����v�p������
    private float jumpForce;                        // �W�����v��
    

    public override void Enter()
    {
        base.Enter();

        jumpForce = playerAction.customJumpForce;

        // �v���C���[�ɃW�����v�͂�K�p
        player.SetVelocityY(jumpForce);

        // �W�����v���̃p�[�e�B�N���G�t�F�N�g���Đ�
        playerParticleController.jumpParticle.Play();

        stateTimer = jumpTime;
    }


    public override void LogicUpdate()
    {
        // ���͂Ɋ�Â��ăv���C���[�̌����𐧌�
        player.FlipController(xInput);

        base.LogicUpdate();
        if(stateMachine.CheckCurrentState(this)) return;
        // �㏸���I���A���~���n�߂��痎����Ԃɐ؂�ւ���
        if (rb.velocity.y <= 0)
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            

        // �ǂɐڐG���Ă��āA�W�����v���͂�����ꍇ�A�ǃW�����v��Ԃɐ؂�ւ���
        if ((playerAction.HasJumpInputBuffer || Jump) && player.IsWallDetected() && stateTimer < 0)
        {
            //�����𔽓]���Ă���
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            return;
        }


    }

}
