using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectWizard : MonoBehaviour
{
    [SerializeField] Transform _circle;
    List<GameObject> _listWizard = new List<GameObject>();

    private void Start()
    {
        for(int i = 0;i< _listWizard.Count;i++)
        {
            _listWizard.Add(_circle.GetChild(i).gameObject);
        }
        SellectAndSetRim(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
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
        for(int i = 0;i< _listWizard.Count; i++)
        {
            if (i == index)
                _listWizard[i].GetComponent<Wizard>().SetRimLight(true);
            else
                _listWizard[i].GetComponent<Wizard>().SetRimLight(false);
        }
    }
}
