using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectWizard : MonoBehaviour
{
    [SerializeField] Transform _circle;
    List<GameObject> _listWizard = new List<GameObject>();

    int _index;
    bool _isStart = false;

    private void Start()
    {
        _index = 0;
        for(int i = 0;i< _circle.childCount; i++)
        {
            _listWizard.Add(_circle.GetChild(i).gameObject);
        }
        SellectAndSetRim(_index);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStart)
        {
            ColorCheck();
            //return;
        }

        else if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
    }

    void ColorCheck()
    {
        int blackCnt = 0;
        foreach (GameObject wizard in _listWizard)
        {
            if (wizard.GetComponent<Wizard>()._isBlack)
                blackCnt++;
        }
        if(blackCnt >= _listWizard.Count - 1)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

    void MouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Wizard wizard = hit.transform.GetComponent<Wizard>();
            if (wizard != null)
            {
                int index = _listWizard.FindIndex(n => n == wizard.gameObject);
                SellectAndSetRim(index);
            }
        }
    }

    void SellectAndSetRim(int index)
    {
        _index = index;
        for(int i = 0;i< _listWizard.Count; i++)
        {
            if (i == _index)
                _listWizard[i].GetComponent<Wizard>().SetRimLight(true);
            else
                _listWizard[i].GetComponent<Wizard>().SetRimLight(false);
        }
    }

    public void StartWizardBtn()
    {
        _isStart = true;
        for (int i = 0; i < _listWizard.Count; i++)
        {
            if (i == _index)
                _listWizard[i].GetComponent<Wizard>().StartWizardBtnOn(true);
            else
                _listWizard[i].GetComponent<Wizard>().StartWizardBtnOn(false);
        }
    }
}
