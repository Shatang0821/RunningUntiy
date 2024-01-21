using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UIInput : Singleton<UIInput>
{
    InputSystemUIInputModule UIInputModule;

    protected override void Awake()
    {
        base.Awake();
        UIInputModule = GetComponent<InputSystemUIInputModule>();
        UIInputModule.enabled = false;
    }

    private void OnEnable()
    {
        EventCenter.Subscribe(InputEvents.disableAllInput, DisableUIInputs);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(InputEvents.disableAllInput, DisableUIInputs);
    }

    /// <summary>
    /// UI��I��
    /// </summary>
    /// <param name="UIObject">�I���\��UI</param>
    /// <remarks>
    ///  UI��I������Ƃ�����InputSystemUIInputModule�L����
    /// </remarks>
    public void SelectUI(Selectable UIObject)
    {
        UIObject.Select();
        UIObject.OnSelect(null);
        UIInputModule.enabled = true;
    }

    public void DisableUIInputs()
    {
        UIInputModule.enabled = false;
    }

    public void EnableUIInputs()
    {
        UIInputModule.enabled = true;
    }

    /// <summary>
    /// Deselect the currently selected UI element.
    /// </summary>
    public void DeselectUI()
    {
        // Clear the current selected UI element
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


}
