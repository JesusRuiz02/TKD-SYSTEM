using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timeLeft = default;
    
    [SerializeField] private TextMeshProUGUI _breakTxt = default; 
    public bool _timerIsOn = true;
   
    private int _actualRound = 1;
    private float _numRounds = default;
    private float _timeBreak = default;
    private float roundDuration = default;
    private float _breakDuration = default;
    [SerializeField] private GameObject _break = default;
    [SerializeField] private TextMeshProUGUI _TimerTxt = default;
    [SerializeField] private TextMeshProUGUI _roundTxt = default;
    [SerializeField] private GameObject DetectionManager = default;
    [SerializeField] private GameObject buttonNextRound;
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

    void Update()
    {
        if (_actualRound > _numRounds)
        {
            _scoreManager.DeterminateWinner();
        }
        if (_timerIsOn)
        {
            _break.SetActive(false);
            DetectionManager.GetComponent<DetectionManager>().BreakOff();
            _TimerTxt.enabled = true;
            RoundTimer();
            UpdateTimer(_timeLeft, _TimerTxt);
        }
        else
        {
            BreakTime();
        }

        if (_actualRound == _numRounds)
        {
            Destroy(buttonNextRound);
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
                _timerIsOn = false;
                if (_5vs5 != null) RoundCheckChange();
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
        _roundTxt.text = _actualRound.ToString("0");
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
        DetectionManager.GetComponent<DetectionManager>().breakOn();
        _TimerTxt.enabled = false;
        UpdateTimer(_timeBreak, _breakTxt);
        _break.SetActive(true);
        if (_timeBreak > 0)
        {
            _timeBreak -= Time.deltaTime;
        }
        else
        {
            _chicharra.Play();
            _timerIsOn = true;
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