using System.Collections.Generic;
using UnityEngine;

// プレイヤーの状態マシンを管理するクラス
public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states; // プレイヤーの状態のリスト

    Player player; // プレイヤーの参照

    [SerializeField] PlayerInput input; // プレイヤーの入力

    //GUIの表示（デバッグ用）
    //void OnGUI()
    //{
    //    Rect rect = new Rect(200, 150, 200, 200);
    //    string message = currentState.ToString();
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 50;
    //    style.fontStyle = FontStyle.Bold;
    //    GUI.Label(rect, message, style);
    //}

    // 起動時の初期化処理
    private void Awake()
    {
        player = GetComponent<Player>(); // プレイヤーのコンポーネントを取得

        // 状態テーブルの初期化
        stateTable = new Dictionary<System.Type, IState>(states.Length);

        // 各状態を初期化し、状態テーブルに追加
        foreach (var state in states)
        {
            state.Initialize(player, this, input); // 各状態の初期化
            stateTable.Add(state.GetType(), state); // 状態テーブルに状態を追加
        }
    }

    // 開始時の処理
    private void Start()
    {
        // 初期状態（アイドル状態）に切り替え
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    // 有効化時の処理
    private void OnEnable()
    {
        // 初期状態（アイドル状態）に切り替え
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    /// <summary>
    /// カスタムジャンプステータスに強制的に変更
    /// </summary>
    public void ForceJumpStateChange(float force)
    {
        EventCenter.TriggerEvent(StateEvents.SetCustomJumpForce, force);
        SwitchState(stateTable[typeof(PlayerCustomJumpState)]);
    }
}
