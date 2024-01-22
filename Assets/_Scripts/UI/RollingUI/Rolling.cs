using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    [SerializeField] private GameObject _optionPrefab;          //ステージプレハブ
    [SerializeField] private Transform _optionGroup;            //ステージの親オブジェクト
    [SerializeField, Range(0f, 10)] private int _optionNum;     //ステージ数
    [SerializeField, Range(1f, 10f)] private float speed;       //回転速度
    [SerializeField, Range(0, 100)] private float _yOffset;     //yオフセット

    private Transform[] _optionsTransform;              //ステージごとの情報
    private float _halfNum;                             //ステージ総数の半分

    private Dictionary<Transform,Vector3> _optionPos = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform,int> _optionIndex = new Dictionary<Transform,int>();       //表示順番

    private Vector3 _center = Vector3.zero;     //回転中心
    private float _r = 500f;                    //回転半径

    private Coroutine currentPIE;

    private void Awake()
    {
        for(int i = 0;i<_optionNum;i++)
        {
            var gameObject = GameObject.Instantiate(_optionPrefab,Vector3.zero,Quaternion.identity,_optionGroup);
            gameObject.name = i.ToString();
        }

        _halfNum = _optionNum / 2;
        _optionsTransform = new Transform[_optionNum];

        for(int i = 0;i<_optionNum;i++)
        {
            _optionsTransform[i] = _optionGroup.GetChild(i);
        }

        InitPos();
        InitSibling();
    }

    private void InitPos()
    {
        float angle = 0;

        for(int i = 0;i<_optionNum;i++)
        {
            angle = (360.0f/ (float)_optionNum) * i * Mathf.Deg2Rad;

            float x = Mathf.Sin(angle)* _r;
            float z = -Mathf.Cos(angle)* _r;

            float y = 0;
            if (i != 0)
            {
                y = i * _yOffset;
                if( i > _halfNum )
                {
                    y = (_optionNum - i) * _yOffset;
                }
            }
            _optionsTransform[i].localPosition = new Vector3 (x, y, z);
            _optionPos.Add(_optionsTransform[i], _optionsTransform[i].localPosition);
        }
    }

    private void InitSibling()
    {
        for(int i = 0;i<_optionNum;i++)
        {
            if (i <= _halfNum)
            {
                if(_optionNum % 2 == 0)
                {
                    _optionsTransform[i].SetSiblingIndex((int)_halfNum - i);
                }
                else
                {
                    _optionsTransform[i].SetSiblingIndex((int)((_optionNum - 1) / 2) - i);
                }
            }
            else
            {
                _optionsTransform[i].SetSiblingIndex(_optionsTransform[_optionNum - i].GetSiblingIndex());
            }
        }

        for(int i = 0; i<_optionNum;i++)
        {
            _optionIndex.Add(_optionsTransform[i], _optionsTransform[i].GetSiblingIndex());
        }
    }

    public void ClickLeft()
    {
        StartCoroutine(MoveLeft());
    }
    
    public void ClickRight()
    {
        StartCoroutine(MoveRight());
    }

    IEnumerator MoveLeft()
    {
        if(currentPIE != null)
        {
            yield return currentPIE;
        }

        Vector3 pos = _optionPos[_optionsTransform[0]];
        int index = _optionIndex[_optionsTransform[0]];
        Vector3 targetPos;

        for(int i = 0;i < _optionNum;i++)
        {
            if(i == _optionNum - 1)
            {
                targetPos = pos;
                _optionIndex[_optionsTransform[i]] = index;
            }
            else
            {
                targetPos = _optionsTransform[(i + 1) % _optionNum].localPosition;
                _optionIndex[_optionsTransform[i]] = _optionIndex[_optionsTransform[(i + 1) % _optionNum]];
            }

            _optionsTransform[i].SetSiblingIndex(_optionIndex[_optionsTransform[i]]);
            currentPIE = StartCoroutine(MoveToTarget(_optionsTransform[i], targetPos));
        }

        yield return null;
    }

    IEnumerator MoveRight()
    {
        if (currentPIE != null)
        {
            yield return currentPIE;
        }

        Vector3 pos = _optionPos[_optionsTransform[_optionNum - 1]];
        int index = _optionIndex[_optionsTransform[_optionNum - 1]];
        Vector3 targetPos;

        for (int i = _optionNum - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                targetPos = pos;
                _optionIndex[_optionsTransform[i]] = index;
            }
            else
            {
                targetPos = _optionsTransform[(i - 1) % _optionNum].localPosition;
                _optionIndex[_optionsTransform[i]] = _optionIndex[_optionsTransform[(i - 1) % _optionNum]];
            }

            _optionsTransform[i].SetSiblingIndex(_optionIndex[_optionsTransform[i]]);
            currentPIE = StartCoroutine(MoveToTarget(_optionsTransform[i], targetPos));
        }

        yield return null;
    }

    IEnumerator MoveToTarget(Transform transform,Vector3 target)
    {
        float tempSpeed = (transform.localPosition - target).magnitude * speed;

        while(transform.localPosition != target)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,target,tempSpeed * Time.deltaTime);
            yield return null;
        }

        _optionPos[transform] = target;

        yield return null;
    }
}
