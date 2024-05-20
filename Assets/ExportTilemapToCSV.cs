using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Text;

public class ExportTilemapToCSV : MonoBehaviour
{
    public Tilemap tilemap; // Inspector����A�T�C������

    private void Start()
    {
        ExportToCSV();
    }

    // CSV�o�͂��g���K�[���郁�\�b�h
    public void ExportToCSV()
    {
        StringBuilder csv = new StringBuilder();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int y = bounds.yMax - 1; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    csv.Append(tile.name); // �^�C�������g�p
                }
                else
                {
                    csv.Append("Empty"); // �^�C�����Ȃ��ꏊ��"Empty"�Ƃ��ďo��
                }
                
                if (x < bounds.xMax - 1)
                    csv.Append(","); // �Z���̋�؂�Ƃ��ăJ���}��ǉ�
            }
            csv.AppendLine(); // �s�̏I���
        }

        string filePath = Path.Combine(Application.dataPath, "ExportedTilemap.csv");
        File.WriteAllText(filePath, csv.ToString());
        Debug.Log("Tilemap exported to CSV at: " + filePath);
    }
}
