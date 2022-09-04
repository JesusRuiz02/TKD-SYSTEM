using System;
using TMPro;
using UnityEngine;

public class ControllerIdentifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI IDTEXT = default;
    [SerializeField] private GameObject _battleManager = default;
    [SerializeField] private int _id = default;

    void Start()
    {
        _battleManager = GameObject.FindGameObjectWithTag("Data");
    }
    
    public void Translation(int id, bool isRed)
    {
        _id = id + 1;
        IDTEXT.text = _id .ToString("0");
        switch (id)
        {
           case 0:
               break;
           case 1:
               if (isRed)
               {
                   transform.Translate(-1f,0,0);
               }
               else
               {
                   transform.Translate(1,0,0);
               }
               break;
           case 2:
               if (isRed)
               {
                   transform.Translate(-2f,0,0);
               }
               else
               {
                   transform.Translate(2f,0,0);
               }
               break;
           case 3:
               if (isRed)
               {
                   transform.Translate(-3f,0,0);
               }
               else
               {
                   transform.Translate(3f,0,0);
               }
               break;
           case 4: 
               if (isRed)
               {
                   transform.Translate(-4f,0,0);
               }
               else
               {
                   transform.Translate(4f,0,0);
               }
               break;
        }
    }

    
}
