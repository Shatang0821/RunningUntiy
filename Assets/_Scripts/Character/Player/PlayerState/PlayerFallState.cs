using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerFallState : PlayerAirState
{
    [SerializeField] float maxFallSpeed;        //�����̍ő呬�x�𐧌�����

    public override void Enter()
    {
        base.Enter();
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
        if(stateMachine.CheckCurrentState(this)) return;
        
        // �n�ʂɒ��n�������`�F�b�N���A���n��Ԃɐ؂�ւ���
        if (player.IsGroundDetected())
        {
            stateMachine.SwitchState(typeof(PlayerLandState));
            return;
        }
           
        // �W�����v���͂�����ꍇ�A�W�����v�o�b�t�@�^�C�}�[��ݒ�
        if (Jump && !player.IsWallDetected())
        {
            playerAction.SetJumpInputBufferTimer();
            return;
        }
            

        // �ǂɐڐG���Ă��āA���͕������v���C���[�̌����Ă�������ƈ�v����ꍇ�A�Ǌ����Ԃɐ؂�ւ���
        if (player.IsWallDetected() && xInput == player.facingDir)
        {
            stateMachine.SwitchState(typeof(PlayerWallSlideState));
            return;
        }
            

        // �ǂɐڐG���Ă��āA�W�����v���͂�����ꍇ�A�ǃW�����v��Ԃɐ؂�ւ���
        if (Jump && player.IsWallDetected())
        {
            //���Α��ɃW�����v���邽�ߌ�����ύX
            player.Flip();
            stateMachine.SwitchState(typeof(PlayerWallJumpState));
            return;
        }

        // �ǂɐڐG���Ă��āA�o����͂�����ꍇ�A�o���Ԃɐ؂�ւ���
        if (player.IsWallDetected() && Climb)
        {
            stateMachine.SwitchState(typeof (PlayerClimbState));  
            return;
        }
              
            
            
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        KillEnemy();

        // �������x�𐧌����鏈���B���x���ő嗎�����x�𒴂��Ȃ��悤�ɒ���
        float newFallSpeed = Mathf.Clamp(rb.velocity.y, maxFallSpeed, float.MaxValue);
        rb.velocity = new Vector2(rb.velocity.x, newFallSpeed);
    }

    private void KillEnemy()
    {
        Collider2D hit = Physics2D.OverlapBox(player.enemyCheck.position, player.attackDistance, 0, player.whatIsEnemy);
        if (hit == null)
            return;
        //hit.gameObject.SetActive(false);
        Enemy enemy = hit.GetComponent<Enemy>();
        stateMachine.SwitchState(typeof(PlayerJumpState));
        enemy.Die();
    }
}