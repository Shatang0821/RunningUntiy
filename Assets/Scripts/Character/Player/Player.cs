using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;  // �ړ����̃p�[�e�B�N��
    public ParticleSystem fallParticle;      // �������̃p�[�e�B�N��
    public ParticleSystem jumpParticle;      // �W�����v���̃p�[�e�B�N��
    public ParticleSystem touchParticle;     // �ڐG���̃p�[�e�B�N��
    [Header("Collision info")]
    [SerializeField] public Transform enemyCheck;         //�n�ʃ`�F�b�N
    [SerializeField] public Vector2 attackDistance;          //�U���ł��鋗��
    [SerializeField] public LayerMask whatIsEnemy;        //���C���[�ݒ�

    [Space]
    public float jumpInputBufferTime = 0.5f; // �W�����v���͂̃o�b�t�@����

    [HideInInspector]
    public WaitForSeconds waitJumpInputBufferTime; // �W�����v���̓o�b�t�@�p�̑ҋ@����

    // �W�����v�֘A�B�L�[��������true�ɂȂ�
    [HideInInspector]
    public bool HasJumpInputBuffer { get; set; }

    // �_�b�V���e
    public GameObject dashGhost;

    [HideInInspector]
    public Sprite sprite => gameObject.GetComponentInChildren<SpriteRenderer>().sprite; // ���݂̃X�v���C�g

    // GUI�̕\���i�f�o�b�O�p�j
    //void OnGUI()
    //{
    //    Rect rect = new Rect(200, 150, 200, 200);
    //    string message = facingDir.ToString();
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 50;
    //    style.fontStyle = FontStyle.Bold;
    //    GUI.Label(rect, message, style);
    //}

    // �N�����̏���������
    protected override void Awake()
    {
        base.Awake();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    // �L�������̏���
    private void OnEnable()
    {
        isDeaded = false;
    }

    // ���������̏���
    private void OnDisable()
    {
        HasJumpInputBuffer = false;
    }

    // �W�����v���̓o�b�t�@�̃^�C�}�[��ݒ肷�鏈��
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    // �W�����v���̓o�b�t�@�̃R���[�`��
    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;

        yield return waitJumpInputBufferTime;

        HasJumpInputBuffer = false;
    }

    #region �Փˏ���


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(enemyCheck.position, attackDistance);
    }

    // �Փˎ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Traps" && !isDeaded) Die();
        if (collision.collider.tag == "Enemy" && !isDeaded) Die();
    }

    // ���S���̏���
    public override void Die()
    {
        base.Die();
        // �J������h�炷
        CameraController.Instance.CameraShake(0.06f, 0.1f);

        GameManager.GameState = GameState.Respawn;
        EventCenter.TriggerEvent(EventNames.SpawnPlayer);
        isDeaded = true;
        //Debug.Log("Die");
    }



    #endregion


}
