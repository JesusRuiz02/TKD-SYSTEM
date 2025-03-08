using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private GameObject buttonNextRound;
    [SerializeField] private GameObject pauseUI = default;
    [SerializeField] private TextMeshProUGUI _TimerTxt = default;
    [SerializeField] private TextMeshProUGUI _roundTxt = default;
    [SerializeField] private TextMeshProUGUI _breakTxt = default;
    [SerializeField] private GameObject _break = default;
    [Header("Score Manager")]
    [SerializeField] private TextMeshProUGUI BlueText = default;
    [SerializeField] private TextMeshProUGUI RedText = default;
    [SerializeField] private GameObject _deciderCanvas = default;
    [SerializeField] private TextMeshProUGUI _redRoundsTXT = default;
    [SerializeField] private TextMeshProUGUI _blueRoundsTXT = default;
    [SerializeField] private TextMeshProUGUI _RedGamJeom = default;
    [SerializeField] private TextMeshProUGUI _BlueGamJeom = default;
    [SerializeField] private TextMeshProUGUI round = default;
    [SerializeField] private GameObject _endCanvas = default;

    [Header("Combat Data")]
    [SerializeField] private TextMeshProUGUI CombatID;
    
    
    #region SingleTone
    private static UIManager _instance;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
       
    }

    private void Start()
    {
        SubscribeToGameManagerCombatState();
    }

    #endregion

    #region CombatState
    private void SubscribeToGameManagerCombatState()//Subscribe to Game Manager to receive Game State notifications when it changes
    {
        GameManager.GetInstance().OnCombatStateChange += OnCombatStateChange;
        OnCombatStateChange(GameManager.GetInstance().GetCurrentCombatState());
    }

    private void OnCombatStateChange(CombatStates _newCombateState)
    {
        switch (_newCombateState )
        {
            case CombatStates.PAUSE_STATE:
                pauseUI.SetActive(true);
                break;
            case CombatStates.COMBAT_STATE:
                pauseUI.SetActive(false);
                _break.SetActive(false);
                _TimerTxt.enabled = true;
                break;
            case CombatStates.RESET_STATE:
                buttonNextRound.SetActive(true);
                _TimerTxt.enabled = true;
                _roundTxt.enabled = true;
                break;
            case CombatStates.BREAK_STATE:
                _TimerTxt.enabled = false;
                _break.SetActive(true);
                _breakTxt.enabled = true;
                break;
            case CombatStates.END_STATE:
                _break.SetActive(false);
                round.enabled = false;
                _endCanvas.SetActive(true);
                _TimerTxt.enabled = false;
                break;
                
        }
    }
    

    #endregion

    public void SetCombatIDText(string combatID)
    {
        CombatID.text = combatID;
    }

    public TextMeshProUGUI GetBlueTextScore()
    {
        return BlueText;
    }

    public TextMeshProUGUI GetRedTextScore()
    {
        return RedText;
    }
    
    public TextMeshProUGUI GetTimerTxt()
    {
        return _TimerTxt;
    }

    public GameObject GetEndCanvas()
    {
        return _endCanvas;
    }

    public TextMeshProUGUI GetRoundTxt()
    {
        return _roundTxt;
    }

    public GameObject GetNextRoundButton()
    {
        return buttonNextRound;
    }

    public TextMeshProUGUI GetBreakTxt()
    {
        return _breakTxt;
    }

    public GameObject GetDeciderCanvas()
    {
        return _deciderCanvas;
    }

    public TextMeshProUGUI GetRedRoundText()
    {
        return _redRoundsTXT;
    }

    public TextMeshProUGUI GetRedGameJeom()
    {
        return _RedGamJeom;
    }
    public TextMeshProUGUI GetBlueGameJeom()
    {
        return _BlueGamJeom;
    }
    
    public TextMeshProUGUI GetBlueRoundText()
    {
        return _blueRoundsTXT;
    }
    
    public void UpdateText(TextMeshProUGUI text, int quantity)
    {
        text.text = quantity.ToString("0");
    }
}
