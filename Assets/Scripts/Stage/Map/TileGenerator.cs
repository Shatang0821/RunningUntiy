using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tileBase; // 
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 spawnPos;

    protected virtual void OnEnable()
    {
        Generator();
    }

    /// <summary>
    /// �w�肵���^�C���Ɏw�肵���v���n�u�𐶐�
    /// </summary>
    protected void Generator()
    {
        Debug.Log("in the method");
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (!tilemap.HasTile(localPlace)) continue;
            Debug.Log("in the Loop");
            TileBase tile = tilemap.GetTile(localPlace);
            if (tile != null && tile == tileBase) // 
            {
                Debug.Log("in the generator");
                Vector3 worldPosition = tilemap.CellToWorld(localPlace);
                // 

                GameObject spawnedObject = PoolManager.Release(prefab, worldPosition += spawnPos);

                //�������ʒu�ɉ�]������
                Matrix4x4 tileMatrix = tilemap.GetTransformMatrix(localPlace);
                spawnedObject.transform.rotation = tileMatrix.rotation;
            }
        }

        tilemap.gameObject.SetActive(false);
    }
}
