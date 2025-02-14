using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TableSystem : MonoBehaviour
{
    [Header("Inputs")]
    private TableManager _tableManager;
    private InputAction _inputPause = default;
    private InputAction _addRedGamJeom = default;
    private InputAction _addBlueGamJeom = default;
    [Header("Table System")]
    [SerializeField] private ScoreManager _scoreManager;
    
    

    private void OnEnable()
    {
        _inputPause = _tableManager.Table.Pause;
        _inputPause.Enable();
        _inputPause.performed += InputPause;
        _addBlueGamJeom = _tableManager.Table.AddBlueGamJeom;
        _addBlueGamJeom.Enable();
        _addBlueGamJeom.performed += AddBlueGamJeom;
        _addRedGamJeom = _tableManager.Table.AddRedGamJeom;
        _addRedGamJeom.Enable();
        _addRedGamJeom.performed += AddRedGamJeom;
    }

    private void OnDisable()
    {
        _inputPause.Disable();
        _addBlueGamJeom.Disable();
        _addRedGamJeom.Disable();
    }

    private void Awake()
    {
        SubscribeToGameManagerCombatState();
        _tableManager = new TableManager();
    }
    
    private void SubscribeToGameManagerCombatState()//Subscribe to Game Manager to receive Game State notifications when it changes
    {
        GameManager.GetInstance().OnCombatStateChange += OnCombatStateChange;
        OnCombatStateChange(GameManager.GetInstance().GetCurrentCombatState());
    }

    private void OnCombatStateChange(CombatStates _newCombateState)
    {
       /* switch (_newCombateState )
        {
            
        }*/
    }

    private void Start()
    {
        _scoreManager = GetComponent<ScoreManager>();
    }
    

    private void AddBlueGamJeom(InputAction.CallbackContext context)
    {
        _scoreManager.AddGamJeum(false);
    }
    private void AddRedGamJeom(InputAction.CallbackContext context)
    {
        _scoreManager.AddGamJeum(true);
    }
    private void InputPause(InputAction.CallbackContext context)
    {
        Toggle();
    }

    private void Toggle()
    {
        switch (GameManager.GetInstance().GetCurrentCombatState())
        {
            case CombatStates.PAUSE_STATE:
                GameManager.GetInstance().ChangeCombatState(CombatStates.COMBAT_STATE);
                    break;
            case CombatStates.COMBAT_STATE:
                GameManager.GetInstance().ChangeCombatState(CombatStates.PAUSE_STATE);
                break;
        }
    }

 
    
    
  
}
