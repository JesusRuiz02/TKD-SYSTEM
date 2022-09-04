using System.Collections;
using System.Xml.Schema;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionManager : MonoBehaviour
{
    [SerializeField] private bool _breakflag = default;
    [SerializeField] private float _windowIsOpened = 1f;
    [SerializeField] private int[] blueCounter = default;
    [SerializeField] private int[] redCounter = default;
    [SerializeField] private GameObject _scoreManager = default;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameObject _battleManager = default;
    [Header("Images")]
    [SerializeField] private GameObject _chestPlateBlue = default;
    [SerializeField] private GameObject _chestPlateRed = default;
    [SerializeField] private GameObject _headBlue = default;
    [SerializeField] private GameObject _headRed = default;
    [SerializeField] private GameObject _TwistBlue = default;
    [SerializeField] private GameObject _TwistRed = default;
    [SerializeField] private GameObject _punchRed = default;
    [SerializeField] private GameObject _punchBlue= default;
    [SerializeField] private int _numberOfJudges = 0;
    [SerializeField] private int minim_votes = default;
    
    
    public void WindowIsOpened(int numAction, bool isRed, int numController)
    {
        if (!isPaused && !_breakflag)
        {
            if (isRed)
            {
                redCounter[numAction]++;
            }
            else
            {
                blueCounter[numAction]++;
            }

            switch (numAction)
            {
                case 0:
                    var directionZero = isRed ? new Vector2(9.5f, 4.12f) : new Vector2(-9.5f, 4.12f);
                    var punchColor = isRed ? _punchRed : _punchBlue;
                    InstanceSymbol(isRed,numController,punchColor,directionZero);
                    break;
                case 1:
                    var directionOne = isRed ? new Vector2(9.5f, 2.02f) : new Vector2(-9.5f, 2.02f);
                    var chestPlateColor = isRed ? _chestPlateRed : _chestPlateBlue;
                    InstanceSymbol(isRed,numController,chestPlateColor,directionOne);
                    break;
                case 2:
                    var directionTwo = isRed ? new Vector2(9.5f, 0.02f) : new Vector2(-9.5f, 0.02f);
                    var headColor = isRed ? _headRed : _headBlue;
                    InstanceSymbol(isRed,numController,headColor,directionTwo);
                    break;
                case 3:
                    var directionThree = isRed ? new Vector2(9.5f, -2.02f) : new Vector2(-9.5f, -2.02f);
                    InstanceSymbol(isRed,numController,_TwistRed,directionThree);
                    break;
            }
            ScorePoints(isRed, numAction);
            StartCoroutine(CounterReset(isRed, numAction));   
        }
    }

    private void ScorePoints(bool isRed, int numAction)
    {
        if (isRed)
        {
                if (redCounter[numAction] == minim_votes)
                {
                    _scoreManager.GetComponent<ScoreManager>().scoreManager(isRed, numAction);
                    StopCoroutine(CounterReset(isRed,numAction));
                    redCounter[numAction] = 0;
                }
        }
        else
        {
                if (blueCounter[numAction] == minim_votes)
                {
                    _scoreManager.GetComponent<ScoreManager>().scoreManager(isRed, numAction);
                    StopCoroutine(CounterReset(isRed,numAction));
                    blueCounter[numAction] = 0;
                }
        }
    }
    
    private void InstanceSymbol( bool isRed, int numController, GameObject images, Vector2 position)
    {
         var image = Instantiate(images, position, quaternion.identity);
       image.GetComponent<ControllerIdentifier>().Translation(numController,isRed);
       Destroy(image, _windowIsOpened);
    }
    private IEnumerator CounterReset(bool isRed, int actionNum)
    {
        yield return new WaitForSeconds(_windowIsOpened);
        if (isRed)
        {
            if (redCounter[actionNum] > 0)
            {
                redCounter[actionNum]--;
            }
        }
        else
        {
            if (blueCounter[actionNum] > 0)
            {
                blueCounter[actionNum]--;
            }
        }
    }

    public void AddReferee()
    {
        _numberOfJudges++;
        RefereeCheck();
    }

    public void EraseReferee()
    {
        _numberOfJudges--;
        RefereeCheck();
    }

    private void RefereeCheck()
    {
        if (_numberOfJudges <= 2)
        {
            minim_votes = 1;
        }
        else if(_numberOfJudges <= 4)
        {
            minim_votes = 2;
        }
        else if (_numberOfJudges >= 5)
        {
            minim_votes = 3;
        }
    }
    
    public void StopReferee()
    {
        isPaused = true;
    }

    public void breakOn()
    {
        _breakflag = true;
    }

    public void BreakOff()
    {
        _breakflag = false;
    }

    public void PlayReferee()
    {
        isPaused = false;
    }

    private void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        _battleManager = GameObject.FindGameObjectWithTag("Data");
        _windowIsOpened = _battleManager.GetComponent<BattleManager>().WindowTime;
    }
}