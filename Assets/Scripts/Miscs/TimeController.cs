using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //�ꎞ��~�O�̃X�P�[�����L�^����
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
    /// timeScale��0�ɂ���input�X�V���[�h�𓮓I�ɕύX
    /// </summary>
    public void Pause()
    {
        currentScale = Time.timeScale;
        Time.timeScale = 0f;

        EventCenter.TriggerEvent(InputEvents.DynamicInput);
    }

    /// <summary>
    /// Pause�J�n����timeScale�ɖ߂�input�X�V���[�h���Œ�ɕύX
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = currentScale;
        EventCenter.TriggerEvent(InputEvents.FixedInput);
    }

}
