using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        UIInput.Instance.DisableUIInputs();
    }

    private void OnDisable()
    {
        UIInput.Instance.EnableUIInputs();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            UIInput.Instance.EnableUIInputs();
        }
    }
}
