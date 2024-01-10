using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
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
    /// timeScale‚ð0‚É‚·‚é
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// PauseŠJŽnŽž‚ÌtimeScale‚É–ß‚é
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = 1;
    }
}
