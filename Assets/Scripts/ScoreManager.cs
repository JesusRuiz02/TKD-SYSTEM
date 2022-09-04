using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    Timer _timer;
    [SerializeField] private GameObject _break = default;
    [SerializeField] private GameObject DataManager = default;
    [SerializeField] private float RedScore = 0;
    [SerializeField] private float BlueScore = 0;
    [SerializeField] private int BlueGameJeum = 0;
    [SerializeField] private int RedGameJeum = 0;
    [SerializeField] private bool _goldenPoint = default;
    [SerializeField] private TextMeshProUGUI _timerTXT = default;
    [SerializeField] private TextMeshProUGUI round = default;
    [SerializeField] private TextMeshProUGUI _RedGamJeom = default;
    [SerializeField] private TextMeshProUGUI _BlueGamJeom = default;
    [SerializeField] private GameObject _endCanvas = default;
    

    [Header("ScoreManager")] [SerializeField]
    private TextMeshProUGUI BlueText = default;

    [SerializeField] private TextMeshProUGUI RedText = default;
    private int punchPoint = default;
    private int helmetPoint = default;
    private int chestplatePoint = default;
    private int twistPoint = default;
    private GoldenPoint _GoldenPoint = default;
    [SerializeField] private GameObject RedColor = default;
    [SerializeField] private GameObject BlueColor = default;
    [SerializeField] private int _gameJeumLimit = default;
    [SerializeField] private int pointsDifference = default;
    [SerializeField] private float difference = default;

    private void Start()
    {
        _timer = GetComponent<Timer>();
        DataManager = GameObject.FindGameObjectWithTag("Data");
        _GoldenPoint = GetComponent<GoldenPoint>();
        punchPoint = DataManager.GetComponent<BattleManager>().PunchPoint;
        helmetPoint = DataManager.GetComponent<BattleManager>().HelmetPoint;
        chestplatePoint = DataManager.GetComponent<BattleManager>().ChestPlatePoint;
        twistPoint = DataManager.GetComponent<BattleManager>().TwistPoint;
        _gameJeumLimit = DataManager.GetComponent<BattleManager>().GameJeum;
        pointsDifference = DataManager.GetComponent<BattleManager>().PointDifference;
    }

    public void scoreManager(bool isRed, int counter)
    {
        switch (counter)
        {
            case 0:
                FistPoint(isRed);
                break;
            case 1:
                BreastPlatePoint(isRed);
                break;
            case 2:
                HeadPoint(isRed);
                break;
            case 3:
                TwistPoint(isRed);
                break;
        }
       DifferenceCheck();
    }

    private void DifferenceCheck()
    {
        difference = Mathf.Abs(RedScore - BlueScore);
        if (difference >= pointsDifference)
        {
            DeterminateWinner();
        }
    }

    private void HeadPoint(bool isRed)
    {
        if (isRed)
        {
            RedScore += helmetPoint;
        }
        else
        {
            BlueScore += helmetPoint;
        }
    }

    private void BreastPlatePoint(bool isRed)
    {
        if (isRed)
        {
            RedScore += chestplatePoint;
        }
        else
        {
            BlueScore += chestplatePoint;
        }
    }

    private void TwistPoint(bool isRed)
    {
        if (isRed)
        {
            RedScore += twistPoint;
        }
        else
        {
            BlueScore += twistPoint;
        }
    }

    private void FistPoint(bool isRed)
    {
        if (isRed)
        {
            RedScore += punchPoint;
        }
        else
        {
            BlueScore += punchPoint;
        }
    }

    private void Update()
    {
        if (_goldenPoint)
        {
            if (RedScore > 1)
            {
                DeterminateWinner();
            }

            if (BlueScore > 1)
            {
                DeterminateWinner();
            }
        }

        RedText.text = RedScore.ToString("0");
        BlueText.text = BlueScore.ToString("0");
        if (BlueGameJeum >= _gameJeumLimit)
        {
            ColorWinner(RedColor);
        }

        if (RedGameJeum >= _gameJeumLimit)
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
        difference = Mathf.Abs(RedScore - BlueScore);
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
        difference = Mathf.Abs(RedScore - BlueScore);
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
            _GoldenPoint.enabled = true;
            _goldenPoint = true;
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