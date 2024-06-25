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
    /// <summary>
    /// カメラの位置を変更するメソッド
    /// </summary>
    /// <param name="target"></param>
    public void ChangeCameraPos(Transform target)
    {
        StartCoroutine(ChangeCameraPosCoroutine(target.position));
    }

    /// <summary>
    /// カメラの位置変更処理
    /// </summary>
    /// <param name="cameraPos"></param>
    /// <returns></returns>
    private IEnumerator ChangeCameraPosCoroutine(Vector3 cameraPos)
    {
        EventCenter.TriggerEvent(TimeEvents.StopTime);
        EventCenter.TriggerEvent(InputEvents.StopGamepadVibration);
        
        while (this.transform.position != cameraPos)
        {
            if (GameManager.GameState == GameState.Playing)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, cameraPos, cameraSpeed * Time.unscaledDeltaTime);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            else
            {
                yield return null;
            }
            
        }
        EventCenter.TriggerEvent(TimeEvents.StartTime);
    }

    #region カメラシェーク
    
    /// <summary>
    /// カメラシェーク
    /// </summary>
    /// <param name="intensity">強さ</param>
    /// <param name="duration">継続時間</param>
    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    // カメラシェークのコルーチン
    private IEnumerator DoCameraShake(float intensity, float duration)
    {
        float elapsed = 0.0f;
        EventCenter.TriggerEvent(InputEvents.GamepadVibration);
        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);

            mainCamera.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        EventCenter.TriggerEvent(InputEvents.StopGamepadVibration);

        mainCamera.transform.position = originalPos;
    }
    #endregion

}
