using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataInfo", menuName = "ScriptableObjects/DataInfo", order = 1)]
public class CombatData : ScriptableObject
{
    [SerializeField] private string _nameEvent = default;
    [SerializeField] private List<CombatInfo> _combatInfos = new List<CombatInfo>();
    private int _area = 0;
    
    [SerializeField] private int combatID = 0;
    public void SetArea(int area)
    {
        _area = area;
    }

    public void Reset()
    {
        _combatInfos.Clear();
        _combatCount = 0;
    }

    public int GetArea()
    {
        return _area;
    }
    private void SetCombatID()
    {
       combatID = _area * 100 + _combatCount;
    }
   private string _date = String.Empty;
   private string GetHour()
   {
       return DateTime.Now.ToString("HH:mm:ss");
   }
   public void SetDate()
   {
       DateTime dateTime = DateTime.Now;
       _date = dateTime.ToString("dd/MM/yyyy");
       
   }

   public String GetDate()
   {
       return _date;
   }
   private int _combatCount = 0;
   public void AddCombat()
   {
       CombatInfo combatInfo = new CombatInfo();
       combatInfo.SetWinnerReason(WinnerReasons.MatchUnfinished);
       _combatCount++; 
       SetCombatID();
       combatInfo.SetCombatID(combatID);
       combatInfo.SetHour(GetHour());
       _combatInfos.Add(combatInfo);
   }

   public List<CombatInfo> GetCombatInfo()
   {
       return _combatInfos;
   }
}

[Serializable]
public class CombatInfo
{
    private string _hour = default;
    public void SetHour(string hour)
    {
        _hour = hour;
    }
    public string GetHour()
    {
        return _hour;
    }
    private int _combatId = 0;
    public int GetCombatID()
    {
        return _combatId;
    }
    public void SetCombatID(int combatID)
    {
         _combatId = combatID;
    }
    private string _combatResult = String.Empty;
    public string GetCombatResult()
    {
        return _combatResult;
    }
    public void SetCombatResult(string result)
    {
        _combatResult = result;
    }
    private string _winner = String.Empty;
    public string GetWinner()
    {
        return _winner;
    }
    public void SetWinner(bool redWins)
    {
        _winner = redWins ? "Red Wins" : "Blue Wins";
    }
    private WinnerReasons _winnerReasons = WinnerReasons.MatchUnfinished;
    public string GetWinnerReasons()
    {
        string reason = "";
        switch (_winnerReasons)
        {
            case WinnerReasons.RoundWinner:
                reason = "Round Difference";
                break;
            case WinnerReasons.MatchUnfinished:
                reason = "Unfinished Match";
                break;
            case WinnerReasons.RefereeDecision:
                reason = "Referee Decision";
                break;
        }

        return reason;
    }
    public void SetWinnerReason(WinnerReasons reasons)
    {
        _winnerReasons = reasons;
    }
    
}

public enum WinnerReasons
{
    RoundWinner,
    RefereeDecision,
    MatchUnfinished
}
