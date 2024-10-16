using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] float _rotSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed * Time.deltaTime, 0));       
    }
}
