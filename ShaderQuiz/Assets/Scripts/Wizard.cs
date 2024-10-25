using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    Renderer _render;
    MaterialPropertyBlock _probBlock;

    public bool _isBlack = false;

    private void Awake()
    {
        _render = GetComponentInChildren<Renderer>();

        _probBlock = new MaterialPropertyBlock();

        _probBlock.SetColor("_Color", RandomColor());

        _render.SetPropertyBlock(_probBlock);
    }

    public void SetRimLight(bool isOn)
    {
        //_probBlock = new MaterialPropertyBlock();
        if (isOn)
            _probBlock.SetFloat("_IsRimOn", 1);
        else
            _probBlock.SetFloat("_IsRimOn", 0);

        _render.SetPropertyBlock(_probBlock);
    }

    Color RandomColor()
    {
        Color color;
        while (true)
        {
            color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            return color;
        }
    }

    public void StartWizardBtnOn(bool isOn)
    {
        if (isOn)
            _probBlock.SetFloat("_Speed", 3);
        else 
        {
            StartCoroutine("NonSellected");
            return;
        }
        _render.SetPropertyBlock(_probBlock);
    }

    IEnumerator NonSellected()
    {
        while (_probBlock.GetColor("_Color") != Color.black)
        {
            Color color = _probBlock.GetColor("_Color");
            color -= (Color.white * Time.deltaTime);

            if (color.a < 0)
                color = Color.black;

            _probBlock.SetColor("_Color", color);
            _render.SetPropertyBlock(_probBlock);
            yield return null;
        }
        _isBlack = true;
    }
}
