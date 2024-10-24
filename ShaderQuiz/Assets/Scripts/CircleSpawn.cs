using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    [Header("캐릭 수"), SerializeField] int _characterCnt; 

    [Header("반지름"), SerializeField] float _radius = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _characterCnt; i++)
        {
            GameObject character = Instantiate(_prefab, transform);

            float angle = Mathf.PI * 0.5f - i * (Mathf.PI * 2.0f) / _characterCnt;

            character.transform.position
                = transform.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * _radius;
        }
    }
}
