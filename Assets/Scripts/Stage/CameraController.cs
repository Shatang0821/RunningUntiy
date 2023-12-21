using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] GameObject[] cameraPos;

    private Camera mainCamera; // ƒƒCƒ“ƒJƒƒ‰‚ÌQÆ‚ğŠi”[

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }

    public void ChangeCamera(int index)
    {
        //Debug.Log("Change");
        mainCamera.transform.position = cameraPos[index].transform.position;
    }

    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(DoCameraShake(intensity, duration));
    }

    private IEnumerator DoCameraShake(float intensity, float duration)
    {
        Vector3 originalPos = Camera.main.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);

            Camera.main.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPos;
    }
}
