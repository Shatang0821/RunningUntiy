using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    float currentScale;
    private void OnEnable()
    {
        EventCenter.Subscribe(InputNames.onPause, Pause);
        EventCenter.Subscribe(ButtonNames.resumeButton, UnPause);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(InputNames.onPause, Pause);
        EventCenter.Unsubscribe(ButtonNames.resumeButton, UnPause);
    }

    /// <summary>
    /// timeScaleを0にする
    /// </summary>
    public void Pause()
    {
        currentScale = Time.timeScale;
        Time.timeScale = 0f;
        EventCenter.TriggerEvent(InputNames.DynamicInput);
    }

    /// <summary>
    /// Pause開始時のtimeScaleに戻る
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = currentScale;

    }

}
