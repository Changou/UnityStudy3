using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellectSlime : MonoBehaviour
{
    List<GameObject> _listSlime = new List<GameObject>();
    int _curNode;
    [SerializeField] Transform _slimeSlot;

    [SerializeField] Cursor _cursor;
    [Header("밝기 정도"), Range(0,1)][SerializeField] float _bright;

    [Header("슬라임 판 회전")]
    [SerializeField] RotationSlime _rotSlime;

    // Start is called before the first frame update
    void Start()
    {
        _curNode = 0;
        for (int i = 0; i < _slimeSlot.childCount; i++)
        {
            _listSlime.Add(_slimeSlot.GetChild(i).gameObject);
        }
        _cursor.transform.position = _listSlime[_curNode].transform.position;
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _curNode = ++_curNode >= _listSlime.Count ? 0 : _curNode;
            MoveSlime();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _curNode = --_curNode < 0 ? _listSlime.Count - 1 : _curNode;
            MoveSlime();
        }
    }

    void MouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Slime slime = hit.transform.GetComponent<Slime>();
            if(slime != null)
            {
                _curNode = _listSlime.FindIndex(n => n == slime.gameObject);
                MoveSlime();
            }
        }
    }

    void MoveSlime()
    {
        _cursor.transform.SetParent(_listSlime[_curNode].transform);
        _cursor.transform.localPosition = Vector3.zero;
        for (int i = 0; i < _listSlime.Count; i++)
        {
            if (_curNode == i)
            {
                _rotSlime._dir = _listSlime[i].GetComponent<Slime>()._DirX > 0 ? -1 : 1;
                    
                _listSlime[i].transform.GetComponent<Slime>().SetSlime(_bright);
                _listSlime[i].transform.GetComponent<Slime>()._rayOn = true;
            }
            else
            {
                _listSlime[i].transform.GetComponent<Slime>().SetSlime(0);
                _listSlime[i].transform.GetComponent<Slime>()._rayOn = false;
            }
        }
        _rotSlime._isRot = true;
        SetInfo();
    }

    void SetInfo()
    {
        _listSlime[_curNode].GetComponent<Slime>().SetSlimeInfo();
    }
}
