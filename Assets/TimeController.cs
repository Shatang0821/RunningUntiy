using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private void OnEnable()
    {
        EventCenter.Subscribe(InputNames.onPause, Pause);
        EventCenter.Subscribe(InputNames.unPause, UnPause);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(InputNames.onPause, Pause);
        EventCenter.Unsubscribe(InputNames.unPause, UnPause);
    }

    /// <summary>
    /// timeScale��0�ɂ���
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Pause�J�n����timeScale�ɖ߂�
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = 1;
    }
}