using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase t_Apple; // 
    [SerializeField] GameObject o_Apple;
    private void Start()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (!tilemap.HasTile(localPlace)) continue;

            TileBase tile = tilemap.GetTile(localPlace);
            if (tile != null && tile == t_Apple) // 
            {
                Vector3 worldPosition = tilemap.CellToWorld(localPlace);
                // 
                PoolManager.Release(o_Apple, worldPosition += new Vector3(0.5f,0.5f,0f));
            }
        }

        tilemap.gameObject.SetActive(false);
    }
}
