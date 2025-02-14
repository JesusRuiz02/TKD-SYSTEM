using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timeLeft = default;
    private int _actualRound = 1;
    private float _numRounds = default;
    private float _timeBreak = default;
    private float roundDuration = default;
    private float _breakDuration = default;
    [SerializeField] private TeamCombatScoreManager _5vs5 = default;
    [SerializeField] private AudioSource _chicharra = default;
    private ScoreManager _scoreManager;

    void Start()
    {
        _chicharra = GetComponent<AudioSource>();
        _scoreManager = GetComponent<ScoreManager>();
        _numRounds = BattleManager.instance.NumberOfRounds;
        roundDuration = BattleManager.instance.MaxTimerInSeconds; 
        _breakDuration = BattleManager.instance.BreakDuration; 
        _timeBreak = _breakDuration;
        _timeLeft = _5vs5 != null ? BattleManager.instance.FirstRoundDuration  *  BattleManager.instance.NumberOfMembers : BattleManager.instance.MaxTimerInSeconds;
        if (_5vs5 != null)
        {
            RepeatSign();
        }
    }


    #region CombatStates
    private void Awake()
    {
        SubscribeToGameManagerCombatState();
    }
    private void SubscribeToGameManagerCombatState()//Subscribe to Game Manager to receive Game State notifications when it changes
    {
        GameManager.GetInstance().OnCombatStateChange += OnCombatStateChange;
        OnCombatStateChange(GameManager.GetInstance().GetCurrentCombatState());
    }

    private void OnCombatStateChange(CombatStates _newCombateState)
    {
        switch (_newCombateState)
        {
            case CombatStates.RESET_STATE:
                _actualRound = 1;
                _timeLeft = roundDuration - 1;
                GameManager.GetInstance().ChangeCombatState(CombatStates.PAUSE_STATE);
                break;
        }
    }
    

    #endregion
  

    void Update()
    {
        if (_actualRound > _numRounds)
        {
            _scoreManager.DeterminateWinner();
        }
        if (GameManager.GetInstance().GetCurrentCombatState() == CombatStates.COMBAT_STATE)
        {
            RoundTimer();
            UpdateTimer(_timeLeft, UIManager.GetInstance().GetTimerTxt());
        }
        else if(GameManager.GetInstance().GetCurrentCombatState() == CombatStates.BREAK_STATE)
        {
            BreakTime();
        }

        if (_actualRound == _numRounds)
        {
           UIManager.GetInstance().GetNextRoundButton().SetActive(false);
        }
    }
    
    private void RoundTimer()
    {
        if (_actualRound <= _numRounds)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _chicharra.Play();
                GameManager.GetInstance().ChangeCombatState(CombatStates.BREAK_STATE);
                if (_5vs5 == null) RoundCheckChange();
                else RoundChange();
                
            }
        }
    }

    private void UpdateTimer(float currentTimer, TextMeshProUGUI cronometer)
    {
        currentTimer += 1;
        float minutes = Mathf.FloorToInt(currentTimer / 60);
        float seconds = Mathf.FloorToInt(currentTimer % 60);
        cronometer.text = String.Format("{0:00} : {1:00}", minutes, seconds);
         UIManager.GetInstance().GetRoundTxt().text = _actualRound.ToString("0");
    }

    public void RoundCheckChange()
    {
        _scoreManager.WinnerRoundCheck();
        RoundChange();
    }

    public void RoundChange()
    {
        if (_actualRound <= _numRounds)
        {
            _actualRound++;
            _timeLeft = roundDuration - 1;
          
        }

        if (_5vs5 != null)
        {
            if (_actualRound > 1)
            {
                Debug.Log("cancela la señal");
                CancelInvoke("ActivateSign");
            }
        }
    }

    private void BreakTime()
    {
        UpdateTimer(_timeBreak, UIManager.GetInstance().GetBreakTxt());
        if (_timeBreak > 0)
        {
            _timeBreak -= Time.deltaTime;
        }
        else
        {
            _chicharra.Play();
            GameManager.GetInstance().ChangeCombatState(CombatStates.COMBAT_STATE);
            _timeBreak = _breakDuration;
        }
    }
    
    private void RepeatSign()
    {
        InvokeRepeating("ActivateSign", BattleManager.instance.FirstRoundDuration ,  BattleManager.instance.FirstRoundDuration);
    }

    private void ActivateSign()
    {
        GetComponent<SwitchSign>().ActivateObject();
        _chicharra.Play();
    }
}