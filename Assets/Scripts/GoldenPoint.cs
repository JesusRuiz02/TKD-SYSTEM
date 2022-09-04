using UnityEngine;
using TMPro;
using System;

public class GoldenPoint : MonoBehaviour
{
   Timer _timer;
   [SerializeField] private GameObject _dataManager = default;
   [SerializeField] private float roundDuration = default;
   [SerializeField] private bool _timerIsOn = false;
   [SerializeField] private GameObject DetectionManager = default;
   [SerializeField] private TextMeshProUGUI _TimerTxt = default;
   [SerializeField] private TextMeshProUGUI _roundTxt = default;
   [SerializeField] private GameObject _break = default;
   [SerializeField] private TextMeshProUGUI _breakTxt = default;
   [SerializeField] private AudioSource _chicharra = default;
   private float _timeBreak = default;
   private float _breakDuration = default;
   private float _timeLeft = default;
   private int _actualRound = 1;
   private int _roundNumber = 1;
    void Start()
    {
        _chicharra = GetComponent<AudioSource>();
        _timer = GetComponent<Timer>();
        _timer.enabled = false;
        _dataManager = GameObject.FindGameObjectWithTag("Data");
        roundDuration = _dataManager.GetComponent<BattleManager>().MaxTimerInSeconds;
        _breakDuration = _dataManager.GetComponent<BattleManager>().BreakDuration;
        _timeBreak = _breakDuration -1;
        _timeLeft = roundDuration;
    }
    void Update()
    {
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
            UpdateTimer(_timeBreak,_breakTxt);
        }
        
    }
    private void RoundTimer()
    {
        if (_actualRound <= _roundNumber)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _timerIsOn = false;
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
    private void BreakTime()
    {
        if (_timeBreak > 0)
        {
            _timeBreak -= Time.deltaTime;
        }
        else
        {
            _chicharra.Play();
            _timerIsOn = true;
            _timeBreak = _breakDuration - 1 ;
        }
    }
}
