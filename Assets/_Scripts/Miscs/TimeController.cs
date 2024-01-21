using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //一時停止前のスケールを記録する
    float currentScale;

    private void OnEnable()
    {
        EventCenter.Subscribe(TimeEvents.StopTime, Pause);
        EventCenter.Subscribe(TimeEvents.StartTime, UnPause);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(TimeEvents.StopTime, Pause);
        EventCenter.Unsubscribe(TimeEvents.StartTime, UnPause);
    }

    /// <summary>
    /// timeScaleを0にするinput更新モードを動的に変更
    /// </summary>
    public void Pause()
    {
        currentScale = Time.timeScale;
        Time.timeScale = 0f;

        EventCenter.TriggerEvent(InputEvents.DynamicInput);
    }

    /// <summary>
    /// Pause開始時のtimeScaleに戻るinput更新モードを固定に変更
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = currentScale;
        EventCenter.TriggerEvent(InputEvents.FixedInput);
    }

}
