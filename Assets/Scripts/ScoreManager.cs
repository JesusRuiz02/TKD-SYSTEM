using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    Timer _timer;

    [Header("Combat Data")] 
    [SerializeField] private CombatData _combatData;
    [SerializeField] private int RedScore = 0;
    [SerializeField] private int BlueScore = 0;
    [SerializeField] private int BlueGameJeum = 0;
    [SerializeField] private int RedGameJeum = 0;
    [SerializeField] private int RedRoundWins = default;
    [SerializeField] private int BlueRoundWins = default;

    public int GetBlueRoundWins()
    {
        return BlueRoundWins;
    }
    public int GetRedRoundWins()
    {
        return RedRoundWins;
    }


    [Header("ScoreManager")] 
    
    [SerializeField] private GameObject RedColor = default;
    [SerializeField] private GameObject BlueColor = default;

    private void SubscribeToGameManagerCombatState()//Subscribe to Game Manager to receive Game State notifications when it changes
    {
        GameManager.GetInstance().OnCombatStateChange += OnCombatStateChange;
        OnCombatStateChange(GameManager.GetInstance().GetCurrentCombatState());
    }

    private void OnCombatStateChange(CombatStates _newCombateState)
    {
        switch (_newCombateState )
        {
            case CombatStates.RESET_STATE:
            ResetAll();
            _combatData.AddCombat();
                break;
        }
    }

    private void ResetAll()
    {
        RestartScore();
        RedRoundWins = 0;
        BlueRoundWins = 0;
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedRoundText(), RedRoundWins);
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueRoundText(), BlueRoundWins);
        RedColor.GetComponent<Renderer>().material.color = Color.red;
        BlueColor.GetComponent<Renderer>().material.color = Color.blue;
        RedColor.GetComponent<BlinkingColor>().enabled = false;
        BlueColor.GetComponent<BlinkingColor>().enabled = false;
    }

    private void Awake()
    {
       SubscribeToGameManagerCombatState();
    }

    private void Start()
    {
        _timer = GetComponent<Timer>();
    }

    public void scoreManager(bool isRed, int counter)
    {
        switch (counter)
        {
            case 0:
                Score(isRed, BattleManager.instance.PunchPoint);
                break;
            case 1:
                Score(isRed, BattleManager.instance.ChestPlatePoint);
                break;
            case 2:
                Score(isRed, BattleManager.instance.HelmetPoint);
                break;
            case 3:
                Score(isRed, BattleManager.instance.TwistPoint);
                break;
        }
       DifferenceCheck();
    }

    private void Score(bool isRed, int pointToScore)
    {
        
        if (isRed)
        {
            RedScore += pointToScore;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedTextScore(), RedScore);
          
        }
        else
        {
            BlueScore += pointToScore;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueTextScore(), BlueScore);
        }
    }

    private void DifferenceCheck()
    {
       float difference = Mathf.Abs(RedScore - BlueScore);
        if (difference >= BattleManager.instance.PointDifference)
        {
            GameManager.GetInstance().ChangeCombatState(CombatStates.BREAK_STATE);
            _timer.RoundCheckChange();
        }
    }
    

    public void AddGamJeum(bool isRed)
    {
        if (isRed)
        {
            Score(false, 1);
            RedGameJeum++;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedGameJeom(), RedGameJeum);
        }
        else
        {
            Score(true, 1);
            BlueGameJeum++;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueGameJeom(), BlueGameJeum);
        }
        if (BlueGameJeum >= BattleManager.instance.GameJeum)
        {
            _timer.RoundChange();
            WinnerRound(true);
            if (RoundDifferenceCheck())
            {
                DeterminateWinner();
            }
            else
            {
                GameManager.GetInstance().ChangeCombatState(CombatStates.BREAK_STATE);
            }
          
        }
        else if (RedGameJeum >= BattleManager.instance.GameJeum)
        {
            _timer.RoundChange();
            WinnerRound(false);
            if (RoundDifferenceCheck())
            {
                DeterminateWinner();
            }
            else
            {
                GameManager.GetInstance().ChangeCombatState(CombatStates.BREAK_STATE);
            }
        }
        else
        {
            DifferenceCheck();
        }
        
    }

    public void RemoveGamJeum(bool isRed)
    {
        if (isRed)
        {
            if (RedGameJeum > 0)
            {
                Score(false, -1);
                RedGameJeum--;
                UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedGameJeom(), RedGameJeum);
            }
        }
        else
        {
            if (BlueGameJeum > 0)
            {
                Score(true, -1);
                BlueGameJeum--;
                UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueGameJeom(), BlueGameJeum);
            }
        }
        DifferenceCheck();
    }

    public void AddPoints(bool isRed)
    {
        if (isRed)
        {
            Score(true, 1);
        }
        else
        {
            Score(false, 1);
        }
        DifferenceCheck();
    }

    public void RemovePoints(bool isRed)
    {
        if (isRed)
        {
            if (RedScore > 0)
            {
                Score(true, -1);
            }
        }
        else
        {
            if (BlueScore > 0)
            {
                Score(false, -1);
            }
        }
        DifferenceCheck();
    }

    public void DeterminateWinner()
    {
        _combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].SetCombatResult(GetBlueRoundWins() + " - " + GetRedRoundWins());
        _combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].SetWinnerReason(WinnerReasons.RoundWinner);
        if (RedRoundWins > BlueRoundWins)
        {
           ColorWinner(RedColor);
          
        }
        else if (BlueRoundWins > RedRoundWins)
        {
            ColorWinner(BlueColor);
        }
        else if (BlueRoundWins == RedRoundWins)
        {
            RestartScore();
            _combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].SetWinnerReason(WinnerReasons.RefereeDecision);
            UIManager.GetInstance().GetDeciderCanvas().SetActive(true);
            StopTimer();
        }
    }

    private void WinnerRound(bool redWins)
    {
        if (redWins)
        {
            RedRoundWins++;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedRoundText(), RedRoundWins);
        }
        else
        {
            BlueRoundWins++;
            UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueRoundText(), BlueRoundWins);
        }
        RestartScore();
        if ( RoundDifferenceCheck())
        {
            DeterminateWinner();
        }

       
    }

    private bool RoundDifferenceCheck()
    {

        return RedRoundWins > (float)BattleManager.instance.NumberOfRounds / 2 ||
                BlueRoundWins > (float)BattleManager.instance.NumberOfRounds / 2;
    }

    public void WinnerRoundCheck()
    {
        if (RedScore > BlueScore)
        {
            WinnerRound(true);
        }
        else if(BlueScore > RedScore)
        {
           WinnerRound(false);
        }
        else if(BlueScore == RedScore)
        {
           WinnerRound(false);
           WinnerRound(true);
        }
    }

    private void RestartScore()
    {
        RedScore = 0;
        BlueScore = 0;
        BlueGameJeum = 0;
        RedGameJeum = 0;
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueTextScore(), BlueScore);
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetBlueGameJeom(), BlueGameJeum);
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedGameJeom(), RedGameJeum);
        UIManager.GetInstance().UpdateText(UIManager.GetInstance().GetRedTextScore(), RedScore);
    }
    

    public void ColorWinner(GameObject winnerColor)
    {
        _combatData.GetCombatInfo()[_combatData.GetCombatInfo().Count - 1].SetWinner(winnerColor.name == "RED");
        winnerColor.GetComponent<BlinkingColor>().enabled = true;
        StopTimer();
        UIManager.GetInstance().GetDeciderCanvas().SetActive(false);
    }
    

    private void StopTimer()
    {
       GameManager.GetInstance().ChangeCombatState(CombatStates.END_STATE);
    }

   
  
}

