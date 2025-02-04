
using UnityEngine;



public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public ViewText viewText;
    [Header("RoundData")]
    [SerializeField] private int _numberOfRounds = 3;
    [SerializeField] private float _maxTimerInSeconds = 90;
    [SerializeField] private int _PointsDifference = 30;
    [Header("CombatData1vs1")]
    [SerializeField] private int _gameJeumLimits = 10;
    [SerializeField] private int _chestPlatePoint = 2;
    [SerializeField] private int _helmetPoint = 3;
    [SerializeField] private int _punchPoint = 1;
    [SerializeField] private int _twistPoint = 4;
    [SerializeField] private float _windowTime = default;
    [SerializeField] private float _breakDuration = 45;
    [Header("CombatData5vs5")]
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
       
       if(instance == null)
       {
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
       
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
        if (viewText != null)
        {
            viewText.ShowText();
        }
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

    public float ChangeData(float dataToChange, bool isIncreasing)
    {
        var addition = 0;
        if (dataToChange > 0)
        {
           addition = isIncreasing ? +1 : -1;
            
        }
        else
        {
            addition = 1;
        }
        dataToChange += addition;
        return dataToChange;
    }

    public void PointsDifference(bool isIncreasing)
    {
       _PointsDifference = (int)ChangeData(_PointsDifference, isIncreasing);
        SaveData();
    }
  
    public void Punch(bool isIncreasing)
    {
        _punchPoint = (int)ChangeData(_punchPoint, isIncreasing);
        SaveData();
    }
    public void Helmet(bool isIncreasing)
    {
        _helmetPoint = (int)ChangeData(_helmetPoint, isIncreasing);
        SaveData();
       
    }
    public void Twist(bool isIncreasing)
    {
       _twistPoint = (int)ChangeData(_twistPoint, isIncreasing);
       SaveData();
    }
    public void Chest(bool isIncreasing)
    {
        _chestPlatePoint = (int)ChangeData(_chestPlatePoint, isIncreasing);
        SaveData();
    }
    public void GamJeum(bool isIncreasing)
    {
        _gameJeumLimits = (int)ChangeData(_gameJeumLimits, isIncreasing);
        SaveData();
    }
    public void RoundNumber(bool isIncreasing)
    {
        _numberOfRounds = (int)ChangeData(_numberOfRounds, isIncreasing);
        SaveData();
    }

    public void ReactionTime(bool isIncreasing)
    {
        _windowTime = (int)ChangeData(_windowTime, isIncreasing);
        SaveData();
    }

    public void BreakIncrease(bool isIncreasing)
    {
        _breakDuration = (int)ChangeData(_breakDuration, isIncreasing);
        SaveData();
    }
    

    public void RoundDuration(bool isIncreasing)
    {
        _maxTimerInSeconds = (int)ChangeData(_maxTimerInSeconds, isIncreasing);
        SaveData();
    }
    public void FirstRDuration(bool isIncreasing)
    {
       _firstRoundEachMember = (int)ChangeData(_firstRoundEachMember, isIncreasing);
        SaveData();
    }

    public void MemberIncrease(bool isIncreasing)
    {
        _numberOfMembers = (int)ChangeData(_numberOfMembers, isIncreasing);
        SaveData();
    }
}
