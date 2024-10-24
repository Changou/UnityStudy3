using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    Renderer _render;
    MaterialPropertyBlock _probBlock;

    private void Awake()
    {
        _render = GetComponentInChildren<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _probBlock = new MaterialPropertyBlock();

        _probBlock.SetColor("_Color", RandomColor());

        _render.SetPropertyBlock(_probBlock);
    }

    public void SetRimLight(bool isOn)
    {
        if(isOn)
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
}
