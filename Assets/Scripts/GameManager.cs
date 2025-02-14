using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private CombatStates _lastCombatStates = CombatStates.PAUSE_STATE;
   [SerializeField] private CombatStates currentCombatState = CombatStates.PAUSE_STATE;
    public Action<CombatStates> OnCombatStateChange;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BackToLastGameState()
    {
        currentCombatState = _lastCombatStates;
    }
    
    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void RestartCombat()
    {
        currentCombatState = CombatStates.RESET_STATE;
        if (OnCombatStateChange != null) 
        {
            OnCombatStateChange.Invoke(currentCombatState);
        }
    }
    
    public void Pairing()
    {
        _lastCombatStates = currentCombatState;
        currentCombatState = CombatStates.PAIRING_STATE;
        if (OnCombatStateChange != null) 
        {
            OnCombatStateChange.Invoke(currentCombatState);
        }
    }
    
    public void ChangeCombatState(CombatStates newCombatState)//When called, the current Game State changes to the new Game State and sends a notification to all subscribers that the Game State changed
    {
        _lastCombatStates = currentCombatState;
        currentCombatState = newCombatState;
         

        if (OnCombatStateChange != null) 
        {
            OnCombatStateChange.Invoke(currentCombatState);
        }
    }

    public CombatStates GetCurrentCombatState()
    {
        return currentCombatState;
    }


}

public enum CombatStates
{
    COMBAT_STATE,
    PAUSE_STATE,
    GOLD_ROUND_STATE,
    END_STATE,
    PAIRING_STATE,
    MEDIC_STATE,
    RESET_STATE,
    BREAK_STATE,
            
}
