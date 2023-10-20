using System;
using UnityEngine;

public class SwitchSign : MonoBehaviour
{
    [SerializeField] private GameObject _switchSign;
    public void ActivateObject()
    {
        _switchSign.SetActive(true);
    }
    
}
