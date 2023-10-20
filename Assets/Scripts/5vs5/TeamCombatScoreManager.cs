using UnityEngine;
using TMPro;

public class TeamCombatScoreManager : MonoBehaviour
{
    Timer _timer;
    [SerializeField] private GameObject _deciderCanvas = default;
    [SerializeField] private GameObject _break = default;
    [SerializeField] private int RedScore = 0;
    [SerializeField] private int BlueScore = 0;
    [SerializeField] private int BlueGameJeum = 0;
    [SerializeField] private int RedGameJeum = 0;
    [SerializeField] private TextMeshProUGUI _timerTXT = default;
    [SerializeField] private TextMeshProUGUI round = default;
    [SerializeField] private TextMeshProUGUI _RedGamJeom = default;
    [SerializeField] private TextMeshProUGUI _BlueGamJeom = default;
    [SerializeField] private GameObject _endCanvas = default;


    [Header("ScoreManager")] 
    [SerializeField] private TextMeshProUGUI BlueText = default;
    [SerializeField] private TextMeshProUGUI RedText = default;
    [SerializeField] private GameObject RedColor = default;
    [SerializeField] private GameObject BlueColor = default;

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
        }
        else
        {
            BlueScore += pointToScore;
        }
    }

    private void DifferenceCheck()
    {
        float difference = Mathf.Abs(RedScore - BlueScore);
        if (difference >= BattleManager.instance.PointDifference)
        {
            DeterminateWinner();
        }
    }
    

    private void Update()
    {
        UpdateText(BlueText, BlueScore);
        UpdateText(RedText, RedScore);
        if (BlueGameJeum >= BattleManager.instance.GameJeum)
        {
            ColorWinner(RedColor);
        }
        if (RedGameJeum >= BattleManager.instance.GameJeum)
        {
            ColorWinner(BlueColor);
        }
    }

    public void AddGamJeum(bool isRed)
    {
        if (isRed)
        {
            BlueScore++;
            RedGameJeum++;
            UpdateText(_RedGamJeom,RedGameJeum);
        }
        else
        {
            RedScore++;
            BlueGameJeum++;
            UpdateText(_BlueGamJeom, BlueGameJeum);
        }
        DifferenceCheck();
    }

    public void RemoveGamJeum(bool isRed)
    {
        if (isRed)
        {
            if (RedGameJeum > 0)
            {
                BlueScore--;
                RedGameJeum--;
                UpdateText(_RedGamJeom,RedGameJeum);
            }
        }
        else
        {
            if (BlueGameJeum > 0)
            {
                RedScore--;
                BlueGameJeum--;
                UpdateText(_BlueGamJeom, BlueGameJeum);
            }
        }
        DifferenceCheck();
    }

    public void AddPoints(bool isRed)
    {
        if (isRed)
        {
            RedScore++;
        }
        else
        {
            BlueScore++;
        }
        DifferenceCheck();
    }

    public void RemovePoints(bool isRed)
    {
        if (isRed)
        {
            if (RedScore > 0)
            {
                RedScore--;
            }
        }
        else
        {
            if (BlueScore > 0)
            {
                BlueScore--;
            }
        }
        DifferenceCheck();
    }

    public void DeterminateWinner()
    {
        if (RedScore > BlueScore)
        {
           ColorWinner(RedColor);
        }
        else if (BlueScore > RedScore)
        {
            ColorWinner(BlueColor);
        }
        else if (BlueScore == RedScore)
        {
            RestartScore();
            _deciderCanvas.SetActive(true);
            StopTimer();
        }
    }
    

    private void RestartScore()
    {
        RedScore = 0;
        BlueScore = 0;
        BlueGameJeum = 0;
        RedGameJeum = 0;
    }
    

    public void ColorWinner(GameObject winnerColor)
    {
        winnerColor.GetComponent<BlinkingColor>().enabled = true;
        StopTimer();
        _endCanvas.SetActive(true);
        _deciderCanvas.SetActive(false);
    }

    private void StopTimer()
    {
        Destroy(_break);
        _timer.enabled = false;
        _timerTXT.enabled = false;
        round.enabled = false;
    }

    private void UpdateText(TextMeshProUGUI text, int quantity)
    {
        text.text = quantity.ToString("0");
    }
}
