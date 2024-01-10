using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] GameObject[] cameraPos;

    private Camera mainCamera; // メインカメラの参照を格納

    // カメラの初期位置を保持
    private Vector3 originalCameraPos;

    public bool isShaking;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;

        // 初期位置を記録
        originalCameraPos = mainCamera.transform.position;
        isShaking = false;
    }

    // カメラの位置を変更するメソッド
    public void ChangeCamera(int index)
    {
        StartCoroutine(DoChangeCamera(index));
    }

    // カメラ移動のコルーチン
    private IEnumerator DoChangeCamera(int index)
    {
        // カメラシェークが終了するまで待機
        yield return new WaitUntil(() => !isShaking);
        // ここでカメラの滑らかな移動などの処理を追加
        mainCamera.transform.position = cameraPos[index].transform.position;

        // 移動が終わったことを示すために待機
        //yield return new WaitForSeconds(0.5f);

    }

    // カメラシェークのメソッド
    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    // カメラシェークのコルーチン
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
