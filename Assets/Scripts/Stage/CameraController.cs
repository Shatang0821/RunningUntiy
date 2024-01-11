using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    private Camera mainCamera; // ���C���J�����̎Q�Ƃ��i�[
    [SerializeField] private float cameraSpeed;
    Vector3 originalPos => this.transform.position + new Vector3(0,0,-10);
    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }
    // �J�����̈ʒu��ύX���郁�\�b�h
    public void ChangeCameraPos(Transform target)
    {
        StartCoroutine(ChangeCameraPosCoroutine(target.position));
    }

    private IEnumerator ChangeCameraPosCoroutine(Vector3 cameraPos)
    {
        //Debug.Log(cameraPos);
        while (this.transform.position != cameraPos)
        {
            //Debug.Log("move");
            this.transform.position = Vector3.MoveTowards(this.transform.position, cameraPos, cameraSpeed * Time.deltaTime);
            yield return null;
        }
    }

    #region �J�����V�F�[�N
    // �J�����V�F�[�N�̃��\�b�h
    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    // �J�����V�F�[�N�̃R���[�`��
    private IEnumerator DoCameraShake(float intensity, float duration)
    {
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
    }
    #endregion

}
