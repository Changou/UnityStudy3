using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    public Action _StartBtnClick;

    public void StartBtnOn()
    {
        _StartBtnClick();
    }
}
