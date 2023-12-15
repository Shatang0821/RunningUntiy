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
    private void Start()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (!tilemap.HasTile(localPlace)) continue;

            TileBase tile = tilemap.GetTile(localPlace);
            if (tile != null && tile == tileBase) // 
            {
                Vector3 worldPosition = tilemap.CellToWorld(localPlace);
                // 
                PoolManager.Release(prefab, worldPosition += spawnPos);
            }
        }

        tilemap.gameObject.SetActive(false);
    }
}
