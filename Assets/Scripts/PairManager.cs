using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PairManager : MonoBehaviour
{
    [SerializeField] private GameObject PairCanvas;
    [SerializeField] private PlayerInputManager _playerInputManager;
    private void Start()
    {
        if (PairCanvas) _playerInputManager.enabled = true;
    }

    public void DeactivatePlayerInputManager()
    {
        _playerInputManager.enabled = false;
    }
}
