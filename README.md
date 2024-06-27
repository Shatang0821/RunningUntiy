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

## ゲームダウンロード
<a href="https://drive.google.com/file/d/1QiQSTChbh0g_XJugiJhidbmwY0Qy1RAP/view?usp=drive_link" target="_blank">ここをクリック</a>

## 技術紹介
#### 目次
- [ステージ](#ステージ)
- [プレイヤの残像](#プレイヤの残像)
- [ステージ](#ステージ)
- [ステージ](#ステージ)
- [ステージ](#ステージ)

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



### プレイヤの残像
プレイヤのダッシュの際に生成した残像はオブジェクトプールを使って効率よく
<div style="display: flex; flex-wrap: nowarp;">
<div style="display: flex; justify-content: space-between;">
      <img src="Image/Player.png" alt="Player" style="width: 75%; margin: 1%;">
    </div>
</div>
<a href = "https://github.com/Shatang0821/RunningUntiy/blob/main/Assets/_Scripts/SystemModules/StageManager.cs" target="_blank">コード</a>