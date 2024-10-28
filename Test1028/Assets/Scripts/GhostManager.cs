using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [Header("반지름"), SerializeField] float _rad;

    [Header("캐릭터 수"), SerializeField] int _characterCnt;

    [SerializeField] GameObject _prefab;

    List<Ghost> _ghostList = new List<Ghost>();

    int _curIdx;

    bool _btnClick = false;

    private void Awake()
    {
        UIManager._Inst._StartBtnClick += SellectGhost;
    }

    void Start()
    {
        _curIdx = 0;
        for(int i = 0;i< _characterCnt; i++)
        {
            GameObject character = Instantiate(_prefab);

            float angle = Mathf.PI * 0.5f - i * (Mathf.PI * 2f) / _characterCnt;

            character.transform.position
                = transform.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * _rad;

            _ghostList.Add(character.GetComponent<Ghost>());
        }
        SetRimLight();
    }

    private void Update()
    {
        if(_btnClick)
        {
            CheckGhostDarker();
        }

        else if (Input.GetMouseButtonDown(0))
        {
            ClickGhost();
        }
    }

    private void ClickGhost()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Ghost ghost = hit.transform.GetComponentInParent<Ghost>();
            if (ghost != null)
            {
                _curIdx = _ghostList.FindIndex(n => n.gameObject == ghost.gameObject);
                SetRimLight();
            }
        }
    }

    void SetRimLight()
    {
        for (int i = 0; i < _characterCnt; i++)
        {
            if (i == _curIdx)
                _ghostList[i].SetRimOn(true);
            else
                _ghostList[i].SetRimOn(false);
        }
    }

    void SellectGhost()
    {
        _btnClick = true;

        for(int i = 0;i< _characterCnt; i++)
        {
            if (i == _curIdx)
                _ghostList[i].SetSellectedGhost(true);
            else
                _ghostList[i].SetSellectedGhost(false);
        }
    }

    void CheckGhostDarker()
    {
        int num = 0;
        foreach (Ghost ghost in _ghostList)
        {
            if (ghost._isDark) num++;
        }
        if (num >= _characterCnt - 1)
        {
            GameManager._Inst.EndGame();
        }
    }
}
