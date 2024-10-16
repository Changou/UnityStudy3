using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellectSlime : MonoBehaviour
{
    List<GameObject> _listSlime = new List<GameObject>();
    int _curNode;

    [SerializeField] Cursor _cursor;
    [Header("¹à±â Á¤µµ"), Range(0,1)][SerializeField] float _bright;

    // Start is called before the first frame update
    void Start()
    {
        _curNode = 0;
        Slime[] slimes = FindObjectsOfType<Slime>();
        for (int i = 0; i < slimes.Length; i++)
        {
            _listSlime.Add(slimes[i].gameObject);
        }
        _cursor.transform.position = _listSlime[_curNode].transform.position;
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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

    void MoveSlime()
    {
        _cursor.transform.position = _listSlime[_curNode].transform.position;
        for (int i = 0; i < _listSlime.Count; i++)
        {
            if (_curNode == i)
            {
                _listSlime[i].transform.GetComponent<Slime>().SetSlime(_bright);
            }
            else
                _listSlime[i].transform.GetComponent<Slime>().SetSlime(0);
        }
        SetInfo();
    }

    void SetInfo()
    {
        _listSlime[_curNode].GetComponent<Slime>().SetSlimeInfo();
    }
}
