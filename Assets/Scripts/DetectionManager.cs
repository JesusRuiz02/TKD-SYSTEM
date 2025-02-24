using System;
using System.Collections;
using System.Xml.Schema;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionManager : MonoBehaviour
{
    [SerializeField] private bool TeamCombat = default; 
    private ScoreManager scoreManager = default;
    private TeamCombatScoreManager teamCombatScoreManager = default;
    [SerializeField] private float _windowIsOpened = 1f;
    [SerializeField] private int[] blueCounter = default;
    [SerializeField] private int[] redCounter = default;
    [SerializeField] private GameObject _scoreManager = default;
    
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

    public void SetNumberOfJudges(int judgesCounts)
    {
        _numberOfJudges = judgesCounts;
    }

    
    public void WindowIsOpened(int numAction, bool isRed, int numController)
    {
        if (GameManager.GetInstance().GetCurrentCombatState() == CombatStates.COMBAT_STATE)
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
                    if (!TeamCombat) scoreManager.scoreManager(true, numAction);
                    else teamCombatScoreManager.scoreManager(true, numAction);
                    StopCoroutine(CounterReset(true,numAction));
                    redCounter[numAction] = 0;
                }
        }
        else
        {
                if (blueCounter[numAction] == minim_votes)
                {
                    if (!TeamCombat) scoreManager.scoreManager(false, numAction);
                    else teamCombatScoreManager.scoreManager(false, numAction);
                    StopCoroutine(CounterReset(false,numAction));
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
                redCounter[actionNum]--;
        }
        else
        {
            if (blueCounter[actionNum] > 0)
                blueCounter[actionNum]--;
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
        float result = _numberOfJudges / 2f;
        minim_votes = Mathf.CeilToInt(result);
        if (_numberOfJudges % 2 == 0)
        {
            minim_votes++;
        }
        
    }
    
    

    private void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        teamCombatScoreManager = TeamCombat ? _scoreManager.GetComponent<TeamCombatScoreManager>() : null;
        scoreManager = !TeamCombat ? _scoreManager.GetComponent<ScoreManager>() : null;
        _windowIsOpened = BattleManager.instance.WindowTime;
    }
}