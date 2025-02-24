using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    [SerializeField] private CombatData _combatData;
    private static GameManager _instance;
    [SerializeField] private ScoreManager _scoreManager;
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

    private void Start()
    {
        _combatData.Reset();
        _combatData.AddCombat();
        _combatData.SetDate();
    }

    public void Exit()
    {
        if (_combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].GetWinnerReasons() == "Unfinished Match")
        {
            _combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].SetCombatResult(_scoreManager.GetBlueRoundWins() + " - " + _scoreManager.GetRedRoundWins());
        }
        if (PlayerPrefs.GetInt("IsExport", 1) == 1)
        {
            ExportCsv(_combatData.GetCombatInfo());
        }
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void ExportCsv(List<CombatInfo> combatInfo)
    {
        List<object[]> data = new List<object[]>
        {
            new object[]{"CombatID", "Result", "Winner", "Winner Reason", "Hour"}
        };
        foreach (CombatInfo combat in combatInfo)
        {
            var obj = new object[]
            {
                combat.GetCombatID(), combat.GetCombatResult(), combat.GetWinner(), combat.GetWinnerReasons(),
                combat.GetHour()
            };
            data.Add(obj);
        }
        
        string directoryPath = Path.Combine(Application.persistentDataPath, "Results");
        
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath,
            PlayerPrefs.GetString("EventName") + "_" + _combatData.GetArea() + "_" + _combatData.GetDate()+ ".csv");
        ExportToCSV(data, filePath);
    }

    private void ExportToCSV(List<object[]> data, string filePath)
    {
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var line in data)
            {
                writer.WriteLine(string.Join(",", Array.ConvertAll(line, x => x.ToString())));
            }
        }
        Debug.Log("Archivo CSV guardado en: " + filePath);
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
