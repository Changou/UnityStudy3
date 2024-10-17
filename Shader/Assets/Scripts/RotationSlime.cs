using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSlime : MonoBehaviour
{
    [Header("회전 속도")]
    [SerializeField] float _rotSpeed;
    int _childCount;

    public bool _isRot;

    public int _dir;

    private void Start()
    {
        _isRot = false;
        _childCount = transform.GetChild(0).childCount;
    }

    void Update()
    {
        if(_isRot)
        {
            transform.Rotate(new Vector3(0, _dir * _rotSpeed * Time.deltaTime, 0));
        }
    }
}
