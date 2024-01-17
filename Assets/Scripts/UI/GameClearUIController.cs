using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUIController : MonoBehaviour
{
    [Header("==== CANVAS ====")]
    [SerializeField] Canvas menusCanvas;

    [Header("==== PLYAER INPUT ====")]
    [SerializeField] Button mainMenuButton;         //メインメニューの戻るボタン
    // Start is called before the first frame update

    private void Start()
    {
        GameManager.GameState = GameState.GameClear;
        UIInput.Instance.SelectUI(mainMenuButton);
    }
    private void OnEnable()
    {
        EventCenter.Subscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(ButtonEvents.mainMenuButton, OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        SceneLoader.Instance.LoadMainMenuScene();
    }
}
