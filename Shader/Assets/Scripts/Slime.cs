using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("슬라임 정보")]
    [SerializeField] string _name;
    [SerializeField] int _atk;
    [SerializeField] int _def;
    [SerializeField] int _spd;

    Material _mat;
    Animator _anim;
    Color _defualt;

    private void Awake()
    {
        _mat = transform.GetComponentInChildren<Renderer>().material;
        _anim = GetComponent<Animator>();
    }

    public void SetSlimeInfo()
    {
        string info = $"이름 : {_name}\n" +
            $"공격력 : {_atk}\n" +
            $"방어력 : {_def}\n" +
            $"민첩성 : {_spd}\n";

        UIManager._Inst.SetText(info);
    }
    
    public void SetSlime(float bright)
    {
        StartCoroutine(Brightness(bright));
        if(bright != 0)
            _anim.SetTrigger("Attack");
    }

    IEnumerator Brightness(float bright)
    {
        float num;
        if (bright != 0)
        {
            num = 0;
            while (num < bright)
            {
                num += Time.deltaTime;
                _mat.SetFloat("_Bright", num);
                yield return null;
            }
        }
        else
        {
            num = _mat.GetFloat("_Bright");
            while(num > bright)
            {
                num -= Time.deltaTime;
                _mat.SetFloat("_Bright", num);
                yield return null;
            }
        }
    }
}
