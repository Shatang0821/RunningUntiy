using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] GameObject[] cameraPos;

    private Camera mainCamera; // ���C���J�����̎Q�Ƃ��i�[

    // �J�����̏����ʒu��ێ�
    private Vector3 originalCameraPos;

    public bool isShaking;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;

        // �����ʒu���L�^
        originalCameraPos = mainCamera.transform.position;
        isShaking = false;
    }

    // �J�����̈ʒu��ύX���郁�\�b�h
    public void ChangeCamera(int index)
    {
        StartCoroutine(DoChangeCamera(index));
    }

    // �J�����ړ��̃R���[�`��
    private IEnumerator DoChangeCamera(int index)
    {
        // �J�����V�F�[�N���I������܂őҋ@
        yield return new WaitUntil(() => !isShaking);
        // �����ŃJ�����̊��炩�Ȉړ��Ȃǂ̏�����ǉ�
        mainCamera.transform.position = cameraPos[index].transform.position;

        // �ړ����I��������Ƃ��������߂ɑҋ@
        //yield return new WaitForSeconds(0.5f);

    }

    // �J�����V�F�[�N�̃��\�b�h
    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    // �J�����V�F�[�N�̃R���[�`��
    private IEnumerator DoCameraShake(float intensity, float duration)
    {
        isShaking = true;
        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);

            mainCamera.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
        isShaking = false;
    }

}
