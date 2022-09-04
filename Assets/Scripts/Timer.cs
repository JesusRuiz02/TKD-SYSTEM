using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timeLeft = default;
    private float _timeBreak = default;
    private float _breakDuration = default;
    private GameObject _dataManager = default;
    private float roundDuration = default;
    [SerializeField] private TextMeshProUGUI _breakTxt = default;
    [SerializeField] private bool _timerIsOn = true;
    [SerializeField] private bool teamCombat = false;
    private int _numRounds = default;
    private int _actualRound = 1;
    [SerializeField] private GameObject _break = default;
    [SerializeField] private TextMeshProUGUI _TimerTxt = default;
    [SerializeField] private TextMeshProUGUI _roundTxt = default;
    [SerializeField] private GameObject DetectionManager = default;
    [SerializeField] private GameObject buttonNextRound;
    [SerializeField] private GameObject _5vs5 = default;
    [SerializeField] private AudioSource _chicharra = default;
    private ScoreManager _scoreManager;

    void Start()
    {
        _chicharra = GetComponent<AudioSource>();
        _scoreManager = GetComponent<ScoreManager>();
        _dataManager = GameObject.FindGameObjectWithTag("Data");
        _numRounds = _dataManager.GetComponent<BattleManager>().NumberOfRounds;
        roundDuration = _dataManager.GetComponent<BattleManager>().MaxTimerInSeconds;
        _breakDuration = _dataManager.GetComponent<BattleManager>().BreakDuration;
        _timeBreak = _breakDuration;
        _timeLeft = teamCombat ? _dataManager.GetComponent<BattleManager>().FirstRoundDuration * _dataManager.GetComponent<BattleManager>().NumberOfMembers : roundDuration;
        if (teamCombat)
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
            DetectionManager.GetComponent<DetectionManager>().breakOn();
            _TimerTxt.enabled = false;
            BreakTime();
            UpdateTimer(_timeBreak, _breakTxt);
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
                RoundChange();
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

    public void RoundChange()
    {
        if (_actualRound <= _numRounds)
        {
            _actualRound++;
            _timeLeft = roundDuration - 1;
        }

        if (teamCombat)
        {
            if (_actualRound > 1)
            {
                CancelInvoke("ActivateSign");
            }
        }
    }

    private void BreakTime()
    {
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

    public void ActivateTeamCombat()
    {
        teamCombat = true;
    }

    private void RepeatSign()
    {
        InvokeRepeating("ActivateSign", _dataManager.GetComponent<BattleManager>().FirstRoundDuration, _dataManager.GetComponent<BattleManager>().FirstRoundDuration);
    }

    private void ActivateSign()
    {
        _5vs5.GetComponent<SwitchSign>().ActivateObject();
        _chicharra.Play();
    }
}