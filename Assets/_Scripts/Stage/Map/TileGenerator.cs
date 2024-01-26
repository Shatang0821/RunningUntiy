using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;       //�g�p����^�C���}�b�v
    [SerializeField] TileBase tileBase;     //��������I�u�W�F�N�g�̃x�[�X�^�C��
    [SerializeField] GameObject prefab;     //��������v���n�u
    [SerializeField] Vector3 spawnPos;      //��������ʒu
    private void Awake()
    {
        Generator(); // �N�����ɃW�F�l���[�^�[�֐����Ăяo��
    }

    /// <summary>
    /// �w�肵���^�C���Ɏw�肵���v���n�u�𐶐�
    /// </summary>
    protected void Generator()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin) // �^�C���}�b�v�̑S�Z�������[�v
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z); // ���݂̃Z���ʒu���擾
            if (!tilemap.HasTile(localPlace)) continue; // �^�C�����Ȃ��ꍇ�̓X�L�b�v

            TileBase tile = tilemap.GetTile(localPlace);
            if (tile != null && tile == tileBase) // �w�肵���^�C�������������ꍇ
            {
                Vector3 worldPosition = tilemap.CellToWorld(localPlace); // �Z���̃��[���h���W���擾

                // �v���n�u�𐶐����Ĉʒu��ݒ�
                GameObject spawnedObject = Instantiate(prefab, worldPosition + spawnPos, Quaternion.identity, this.transform);

                // �^�C���̕ϊ��s����g�p���Đ������ʒu�ɉ�]������
                Matrix4x4 tileMatrix = tilemap.GetTransformMatrix(localPlace);
                spawnedObject.transform.rotation = tileMatrix.rotation;
            }
        }

        tilemap.gameObject.SetActive(false); // �^�C���}�b�v���\���ɂ���
    }
}
