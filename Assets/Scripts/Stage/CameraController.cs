using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    private Camera mainCamera; // メインカメラの参照を格納
    [SerializeField] private float cameraSpeed;
    Vector3 originalPos => this.transform.position + new Vector3(0,0,-10);
    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }
    // カメラの位置を変更するメソッド
    public void ChangeCameraPos(Transform target)
    {
        StartCoroutine(ChangeCameraPosCoroutine(target.position));
    }

    private IEnumerator ChangeCameraPosCoroutine(Vector3 cameraPos)
    {
        Time.timeScale = 0;
        EventCenter.TriggerEvent(InputNames.DynamicInput);
        //Debug.Log(cameraPos);
        while (this.transform.position != cameraPos)
        {
            if (GameManager.GameState == GameState.Playing)
            {
                //Debug.Log("move");
                this.transform.position = Vector3.MoveTowards(this.transform.position, cameraPos, cameraSpeed * Time.unscaledDeltaTime);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            else
            {
                yield return null;
            }
            
        }
        Time.timeScale = 1;
        EventCenter.TriggerEvent(InputNames.FixedInput);
    }

    #region カメラシェーク
    // カメラシェークのメソッド
    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    // カメラシェークのコルーチン
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
