using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("==== PARTICLE ====")]
    public ParticleSystem movementParticle;  // �ړ����̃p�[�e�B�N��
    public ParticleSystem fallParticle;      // �������̃p�[�e�B�N��
    public ParticleSystem jumpParticle;      // �W�����v���̃p�[�e�B�N��
    public ParticleSystem touchParticle;     // �ڐG���̃p�[�e�B�N��
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

    [Header("==== DEATH ====")]
    public GameObject DeathVFX; // ���S���̃G�t�F�N�g
    private bool isDeaded; // ���S��Ԃ�\���t���O

    // GUI�̕\���i�f�o�b�O�p�j
    void OnGUI()
    {
        Rect rect = new Rect(200, 150, 200, 200);
        string message = facingDir.ToString();
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(rect, message, style);
    }

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

    #region ���S����

    // �Փˎ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Traps")
        {
            if (!isDeaded)
            {
                Die();
                isDeaded = true;
                Debug.Log("Die");
            }
        }
    }

    // ���S���̏���
    void Die()
    {
        GameManager.GameState = GameState.Respawn;
        PoolManager.Release(DeathVFX, transform.position);
        this.gameObject.SetActive(false);

        EventCenter.TriggerEvent(EventNames.SpawnPlayer);
    }

    #endregion
}
