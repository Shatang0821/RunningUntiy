# RunningMan

## 概要
プレイヤを操作してステージをクリアするゲーム

## ゲーム画面
<div style="display: flex; flex-wrap: wrap;">
  <img src="Image/RunningMan01.png" alt="スクリーンショット1" style="width: 45%; margin: 1%;">
  <img src="Image/RunningMan02.png" alt="スクリーンショット2" style="width: 45%; margin: 1%;">
  <img src="Image/RunningMan03.png" alt="スクリーンショット3" style="width: 45%; margin: 1%;">
  <img src="Image/RunningMan04.png" alt="スクリーンショット4" style="width: 45%; margin: 1%;">
</div>

**ゲーム紹介動画**

[![ゲーム紹介動画](Image/Samune.png)](https://www.youtube.com/watch?v=QjvSPw2S2VY)


## ゲーム実行ファイルのダウンロード

パッド(Xboxの方が望ましい)のみ対応しています

<a href="https://drive.google.com/file/d/1QiQSTChbh0g_XJugiJhidbmwY0Qy1RAP/view?usp=drive_link" target="_blank">ここをクリック</a>

## 技術紹介
#### 目次
- [キャラクター](#キャラクター)
- [ステージ](#ステージ)
- [オブジェクトプール](#オブジェクトプール)
- [タイルマップ](#タイルマップ)
- [インスペクター拡張](#インスペクター拡張)
- [その他](#その他)

---

### キャラクター
Playerはステートマシンを使って制御しています

パラメータが編集しやすくするためScriptableObjectを使って実行しながらパラメータ調整できます

<div style="display: flex; flex-wrap: wrap;">
  <img src="Image/Player01.png" alt="スクリーンショット1" style="width: 45%; margin: 1%;">
  <img src="Image/Player02.png" alt="スクリーンショット2" style="width: 45%; margin: 1%;">
</div>

**ジャンプ入力バッファ** と **コヨテタイム**(プレイヤーがプラットフォームの端から少しはみ出しても一定時間ジャンプできる機能を指します)
<div style="display: flex; flex-wrap: wrap;">
  <img src="Image/PlayerGif001.gif" alt="Player GIF" style="width: 48%; margin: 1%;">
<img src="Image/PlayerGif002.gif" alt="Player GIF" style="width: 48%; margin: 1%;">
</div>

<a href = "https://github.com/Shatang0821/RunningUntiy/tree/main/Assets/_Scripts/Character/Player/PlayerStateMachine" target="_blank" rel="noopener noreferrer">詳細コード</a>

---

### ステージ
プレイヤの前後のステージだけ描画してパフォーマンスの最適化
<div style="display: flex; flex-wrap: wrap;">
  <div style="width: 100%; margin: 1%;">
    <strong style="color: blue;">エディタモードのステージ</strong>
    <img src="Image/Stage02.png" alt="スクリーンショット3" style="width: 100%; margin: 1%;">
  </div>
  <div style="width: 100%; margin: 1%;">
    <strong style="color: blue;">ゲームモードのステージ</strong>
    <div style="display: flex; justify-content: space-between;">
      <img src="Image/Stage00.png" alt="スクリーンショット1" style="width: 49%; margin: 1%;">
      <img src="Image/Stage01.png" alt="スクリーンショット2" style="width: 49%; margin: 1%;">
    </div>
  </div>
</div>
<a href = "https://github.com/Shatang0821/RunningUntiy/blob/main/Assets/_Scripts/SystemModules/StageManager.cs" target="_blank" rel="noopener noreferrer">詳細コード</a>

---

### オブジェクトプール
インスペクターで簡単に設定できる作り方です
<div style="display: flex; justify-content: space-between;">
      <img src="Image/Pool.png" alt="Player" style="width: 75%; margin: 1%;">
</div>

プレイヤがダッシュの際に生成した残像はオブジェクトプールを使って頻繁な生成と削除を防ぎます
<div style="display: flex; flex-wrap: nowarp;">

<div style="display: flex; justify-content: space-between;">
      <img src="Image/Player.png" alt="Player" style="width: 75%; margin: 1%;">
    </div>
</div>

<a href = "https://github.com/Shatang0821/RunningUntiy/tree/main/Assets/_Scripts/Pool" target="_blank" rel="noopener noreferrer">詳細コード</a>

---

### タイルマップ
Unityのタイルマップを応用して、タイルを置くだけで静的なギミックの生成もしくはアイテムの生成ができます
<div style="display: flex; flex-wrap: wrap;">
  <div style="display: flex; justify-content: space-between; flex-wrap: wrap; width: 100%; margin: 1%;">
      <img src="Image/TileMap00.png" alt="TileMap00" style="width: 49%; margin: 1%;">
      <img src="Image/TileMap01.png" alt="TileMap01" style="width: 75%; margin: 1%;">
  </div>
</div>
<a href = "https://github.com/Shatang0821/RunningUntiy/blob/main/Assets/_Scripts/Stage/Map/TileGenerator.cs" target="_blank" rel="noopener noreferrer">詳細コード</a>

---

### インスペクター拡張
レベルデザイナーがカッタートラップが配置しやすくするためインスペクター拡張しました
<div style="display: flex; flex-wrap: wrap;">
  <img src="Image/Trap00.png" alt="スクリーンショット2" style="width: 45%; margin: 1%;">
</div>

**新しい通過点を追加** をクリックすると自動でシーンに通過ポイントが生成されて、位置調整ができます

<img src="Image/Trap01.png" alt="スクリーンショット2" style="width: 10%; height: 5%; margin: 1%;">

<a href = "https://github.com/Shatang0821/RunningUntiy/blob/main/Assets/_Scripts/Editor/CustomEdit/PatrolPointsEditor.cs" target="_blank" rel="noopener noreferrer">詳細コード</a>

---

### その他

<details>
  <summary>イベント集中管理システム</summary>
  クラス間の結合度を避けるために使われるシステムとなります。

  文字列をキーとして使っています
  <a href = "https://github.com/Shatang0821/RunningUntiy/tree/main/Assets/_Scripts/SystemModules/EventCenter" target="_blank" rel="noopener noreferrer">詳細コード</a>
</details>

<details>
  <summary>音マネージャークラス</summary>
  Audioクラスは音源と音量のデータを管理するクラスです。

  効果音を再生するなどの処理をまとめたクラスで、APIとして使用できます。

  <a href = "https://github.com/Shatang0821/RunningUntiy/blob/main/Assets/_Scripts/SystemModules/AudioManager.cs" target="_blank" rel="noopener noreferrer">詳細コード</a>
</details>


