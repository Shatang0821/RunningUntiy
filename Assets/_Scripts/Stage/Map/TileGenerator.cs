using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;       //使用するタイルマップ
    [SerializeField] TileBase tileBase;     //生成するオブジェクトのベースタイル
    [SerializeField] GameObject prefab;     //生成するプレハブ
    [SerializeField] Vector3 spawnPos;      //生成する位置
    private void Awake()
    {
        Generator(); // 起動時にジェネレーター関数を呼び出す
    }

    /// <summary>
    /// 指定したタイルに指定したプレハブを生成
    /// </summary>
    protected void Generator()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin) // タイルマップの全セルをループ
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z); // 現在のセル位置を取得
            if (!tilemap.HasTile(localPlace)) continue; // タイルがない場合はスキップ

            TileBase tile = tilemap.GetTile(localPlace);
            if (tile != null && tile == tileBase) // 指定したタイルが見つかった場合
            {
                Vector3 worldPosition = tilemap.CellToWorld(localPlace); // セルのワールド座標を取得

                // プレハブを生成して位置を設定
                GameObject spawnedObject = Instantiate(prefab, worldPosition + spawnPos, Quaternion.identity, this.transform);

                // タイルの変換行列を使用して正しい位置に回転させる
                Matrix4x4 tileMatrix = tilemap.GetTransformMatrix(localPlace);
                spawnedObject.transform.rotation = tileMatrix.rotation;
            }
        }

        tilemap.gameObject.SetActive(false); // タイルマップを非表示にする
    }
}
