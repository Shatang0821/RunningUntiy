using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/ClimbLeap", fileName = "PlayerState_ClimbLeap")]
public class PlayerClimbLeapState : PlayerAirState
{
    [Header("Jump info")]
    [SerializeField] private float JumpForce = 2;    // �W�����v��
    [SerializeField] private float jumpTime = 0.3f;

    public override void Enter()
    {
        base.Enter();

        // �v���C���[�ɃW�����v�͂�K�p
        player.SetVelocityY(JumpForce);

        // �W�����v���̃p�[�e�B�N���G�t�F�N�g���Đ�
        playerParticleController.jumpParticle.Play();

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
        if(stateMachine.CheckCurrentState(this))
            return;
        // �㏸���I���A���~���n�߂��痎����Ԃɐ؂�ւ���
        if (rb.velocity.y <= 0)
        {
            stateMachine.SwitchState(typeof(PlayerFallState));
            return;
        }
            

        // �ǂɐڐG���Ă��āA�W�����v���͂�����ꍇ�A�ǃW�����v��Ԃɐ؂�ւ���
        if (Jump && player.IsWallDetected() && stateTimer < 0)
        {
            //�����𔽓]���Ă���
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            return;
        }


    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
