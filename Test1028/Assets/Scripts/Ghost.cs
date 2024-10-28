using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Renderer _renderer;
    MaterialPropertyBlock _mpb;
    Animator _anim;

    public bool _isDark = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _renderer = GetComponentInChildren<Renderer>();
        _mpb = new MaterialPropertyBlock();
        SetRandomColor();
    }

    void SetRandomColor()
    {
        _mpb.SetColor("_Color", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        _renderer.SetPropertyBlock(_mpb);
    }

    public void SetRimOn(bool isOn)
    {
        if (isOn)
            _mpb.SetFloat("_RimOn", 1);
        else
            _mpb.SetFloat("_RimOn", 0);

        _renderer.SetPropertyBlock(_mpb);
    }

    public void SetSellectedGhost(bool isOn)
    {
        if (isOn)
        {
            _anim.SetTrigger("SellectGhost");
            _mpb.SetFloat("_BlinkOn", 1);
            _renderer.SetPropertyBlock(_mpb);
        }
        else
        {
            StartCoroutine("DarkerColor");
        }
    }

    IEnumerator DarkerColor()
    {
        while (_mpb.GetColor("_Color") != Color.black)
        {
            Color color = _mpb.GetColor("_Color");
            color -= Color.white * Time.deltaTime;
            if(color.a <= 0f)
            {
                color = Color.black;
            }
            _mpb.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_mpb);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        _isDark = true;
    }
}
