using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PoolManager : MonoBehaviour
{

    [SerializeField] Pool[] dashGhost;
    [SerializeField] Pool[] apple;

    // �e�v���n�u�Ƃ���Ɋ֘A����I�u�W�F�N�g�v�[�����֘A�t���邽�߂̎����B
    static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(dashGhost);
        Initialize(apple);
    }

    // UNITY_EDITOR�f�B���N�e�B�u�́AUnity�G�f�B�^�����ł̂݃R�[�h�����s���邽�߂̂��́B
    // ���ۂ̃Q�[���v���C�ł͎��s����Ȃ��B
#if UNITY_EDITOR
    void OnDestroy()
    {
        //�v�[���T�C�Y�������������`�F�b�N����
        CheckPoolSize(dashGhost);
        CheckPoolSize(apple);
    }
#endif

    // �e�v�[���̃����^�C�����̃T�C�Y���m�F���A�����ݒ�����傫���ꍇ�͌x����\������B
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("�v�[��:{0}�͏����T�C�Y{2}���{1}�̕���������!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }


    // �v�[�������������A���ꂼ��̃v�[���������ɒǉ�����B
    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR    
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("������`���Ă���v���n�u�����܂� prefab:" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);

            // Hierarchy�r���[�Ō��₷�����邽�߂ɐV����GameObject���쐬���āA���̎q�Ƃ��ăv�[���I�u�W�F�N�g�����B
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <param name="position">
    /// <para>�w�肳�ꂽ�����ʒu</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);
    }

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <param name="position">
    /// <para>�w�肳�ꂽ�����ʒu</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation,Sprite sprite,float alpha)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position,rotation,sprite,alpha);
    }
}
