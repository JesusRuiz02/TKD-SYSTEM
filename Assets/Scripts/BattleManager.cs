using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class BattleManager : MonoBehaviour
{
    [Header("RoundData")]
    [SerializeField] private int _numberOfRounds = 3;
    [SerializeField] private float _maxTimerInSeconds = 90;
    [SerializeField] private int _PointsDifference = 30;
    [Header("CombatData")]
    [SerializeField] private int _gameJeumLimits = 10;
    [SerializeField] private int _chestPlatePoint = 2;
    [SerializeField] private int _helmetPoint = 3;
    [SerializeField] private int _punchPoint = 1;
    [SerializeField] private int _twistPoint = 4;
    [SerializeField] private float _windowTime = default;
    [SerializeField] private float _breakDuration = 45;
    [SerializeField] private float _firstRoundEachMember = 90;
    [SerializeField] private int _numberOfMembers = 3;

    public float BreakDuration => _breakDuration;
    public float WindowTime => _windowTime;
    public int GameJeum => _gameJeumLimits;
    public int PointDifference => _PointsDifference;
    public int ChestPlatePoint => _chestPlatePoint;
    public int HelmetPoint => _helmetPoint;
    public int PunchPoint => _punchPoint;
    public int TwistPoint => _twistPoint;
    public float MaxTimerInSeconds => _maxTimerInSeconds;
    public int NumberOfRounds => _numberOfRounds;

    public int NumberOfMembers => _numberOfMembers;
    public float FirstRoundDuration => _firstRoundEachMember;
    
    private void OnDestroy()
    {
         SaveData();
    }

    private void Awake()
    {
       LoadData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("numberOfRounds", _numberOfRounds);
        PlayerPrefs.SetFloat("maxTimer", _maxTimerInSeconds);
        PlayerPrefs.SetInt("pointDifference", _PointsDifference);
        PlayerPrefs.SetInt("gamJeumLimit", _gameJeumLimits);
        PlayerPrefs.SetInt("chestplatePoint", _chestPlatePoint);
        PlayerPrefs.SetInt("helmetPoint", _helmetPoint);
        PlayerPrefs.SetInt("punchPoint", _punchPoint);
        PlayerPrefs.SetInt("twistPoint", _twistPoint);
        PlayerPrefs.SetFloat("windowTime",_windowTime);
        PlayerPrefs.SetFloat("breakDuration", _breakDuration);
        PlayerPrefs.SetFloat("firstRoundDuration", _firstRoundEachMember);
        PlayerPrefs.SetInt("NumberOfMember", _numberOfMembers);
    }

    private void LoadData()
    {
        _numberOfRounds = PlayerPrefs.GetInt("numberOfRounds", 3);
        _maxTimerInSeconds = PlayerPrefs.GetFloat("maxTimer",90);
        _PointsDifference = PlayerPrefs.GetInt("pointDifference", 30);
        _gameJeumLimits = PlayerPrefs.GetInt("gamJeumLimit", 10);
        _chestPlatePoint = PlayerPrefs.GetInt("chestplatePoint", 2);
        _helmetPoint = PlayerPrefs.GetInt("helmetPoint", 3);
        _punchPoint = PlayerPrefs.GetInt("punchPoint", 1);
        _twistPoint = PlayerPrefs.GetInt("twistPoint", 4);
        _windowTime = PlayerPrefs.GetFloat("windowTime", 1f);
        _breakDuration = PlayerPrefs.GetFloat("breakDuration", 45);
        _firstRoundEachMember = PlayerPrefs.GetFloat("firstRoundDuration", 60);
        _numberOfMembers = PlayerPrefs.GetInt("NumberOfMember", 3);
    }

    public void PointsDifference(bool insIncreasing)
    {
        if (_PointsDifference > 0)
        {
            var addition = insIncreasing ? +1 : -1;
            _PointsDifference += addition;
        }
        else
        {
            var addition = 1;
            _PointsDifference += addition; 
        }
        SaveData();
    }
  
    public void Punch(bool insIncreasing)
    {
        if (_punchPoint > 0)
        {
            var addition = insIncreasing ? +1 : -1;
            _punchPoint += addition;
        }
        else
        {
            var addition = 1;
            _punchPoint += addition; 
        }
        SaveData();
    }
    public void Helmet(bool insIncreasing)
    {
        if (_helmetPoint > 0)
        {
            var addition = insIncreasing ? +1 : -1;
            _helmetPoint += addition;
        }
        else
        {
            var addition = 1;
            _helmetPoint += addition; 
        }
        SaveData();
    }
    public void Twist(bool insIncreasing)
    {
        if (_twistPoint > 0)
        {
            var addition = insIncreasing ? +1 : -1;
            _twistPoint += addition; 
        }
        else
        {
            var addition = 1;
           _twistPoint += addition;
        }
        SaveData();
    }
    public void Chest(bool isIncreasing)
    {
        if (_chestPlatePoint > 0)
        {
            var addition = isIncreasing ? +1 : -1;
            _chestPlatePoint += addition; 
        }
        else
        {
            var addition = 1;
            _chestPlatePoint += addition;
        }
        SaveData();
    }
    public void GamJeum(bool isIncreasing)
    {
        if (_gameJeumLimits > 0)
        {
            var addition = isIncreasing ? +1 : -1;
            _gameJeumLimits += addition;
        }
        else
        {
            var addition = 1;
            _gameJeumLimits += addition; 
        }
        SaveData();
    }
    public void RoundNumber(bool isIncreasing)
    {
        if (_numberOfRounds > 0)
        {
            var addition = isIncreasing ? +1 : -1;
            _numberOfRounds += addition;
        }
        else
        {
            var addition = 1;
            _numberOfRounds += addition; 
        }
        SaveData();
    }

    public void ReactionTime(bool isIncreasing)
    {
        if (_windowTime > 0)
        {
            var addition = isIncreasing ? + 0.25f : -0.25f;
            _windowTime += addition;
        }
        else
        {
            var addition = 0.25f;
            _windowTime += addition; 
        }
        SaveData();
    }

    public void BreakIncrease(bool isIncreasing)
    {
        if (_breakDuration > 0)
        {
            var addition = isIncreasing ? +5 : -5;
           _breakDuration += addition;
        }
        else
        {
            var addition = 5;
            _breakDuration += addition; 
        }
        SaveData();
    }
    

    public void RoundDuration(bool isIncreasing)
    {
        if (_maxTimerInSeconds > 0)
        {
            var addition = isIncreasing ? +5 : -5;
            _maxTimerInSeconds += addition;
        }
        else
        {
            var addition = 5;
            _maxTimerInSeconds += addition; 
        }
        SaveData();
    }
    public void FirstRDuration(bool isIncreasing)
    {
        if (_firstRoundEachMember > 0)
        {
            var addition = isIncreasing ? +5 : -5;
            _firstRoundEachMember += addition;
        }
        else
        {
            var addition = 1;
            _firstRoundEachMember += addition; 
        }
        SaveData();
    }

    public void MemberIncrease(bool isIncreasing)
    {
        if (_numberOfMembers > 0)
        {
            var addition = isIncreasing ? +1 : -1;
           _numberOfMembers += addition;
        }
        else
        {
            var addition = 1;
            _numberOfMembers += addition; 
        }
        SaveData();
    }
    private void DataIncrease(int datatype, bool isIncreasing)
    {
        if (datatype > 0)
        {
            var addition = isIncreasing ? +1 : -1;
            datatype += addition; 
        }
        else
        {
            var addition = 1;
            datatype += addition; 
        }
        Debug.Log(datatype);
        SaveData();
        Debug.Log(datatype);
    }
}
