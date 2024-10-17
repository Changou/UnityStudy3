using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleRot : MonoBehaviour
{
    [Header("반지름"), SerializeField] float _radius = 2.0f;

    int numOfChild;

    [Header("오프셋"), SerializeField] float _rotY;

    void Start()
    {
        numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / numOfChild - Mathf.PI * 0.5f;

            GameObject child = transform.GetChild(i).gameObject;

            child.transform.position 
                = transform.position + (new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle))) * _radius;
        }
        transform.rotation = Quaternion.Euler(0, _rotY, 0);
    }
}
