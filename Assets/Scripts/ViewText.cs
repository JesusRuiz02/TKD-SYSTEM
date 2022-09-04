using TMPro;
using UnityEngine;

public class ViewText : MonoBehaviour
{
    private BattleManager _battleManager;
    
    [SerializeField] private TextMeshProUGUI punch = default;
    [SerializeField] private TextMeshProUGUI rounds = default;
    [SerializeField] private TextMeshProUGUI difference = default;
    [SerializeField] private TextMeshProUGUI gameJeum = default;
    [SerializeField] private TextMeshProUGUI twist = default;
    [SerializeField] private TextMeshProUGUI helmet = default;
    [SerializeField] private TextMeshProUGUI chestPlate = default;
    [SerializeField] private TextMeshProUGUI breakduration = default;
    [SerializeField] private TextMeshProUGUI durationround = default;
    [SerializeField] private TextMeshProUGUI reactionTime = default;
    [SerializeField] private TextMeshProUGUI firstRound = default;
    [SerializeField] private TextMeshProUGUI eachMember = default;

    private void Start()
    {
        _battleManager = GetComponent<BattleManager>();
    }

    private void Update()
    {
        ShowText();
    }

    private void ShowText()
    {
        punch.text = _battleManager.PunchPoint.ToString("0");
        rounds.text = _battleManager.NumberOfRounds.ToString("0");
        difference.text = _battleManager.PointDifference.ToString("0");
        gameJeum.text = _battleManager.GameJeum.ToString("0");
        chestPlate.text = _battleManager.ChestPlatePoint.ToString("0");
        twist.text = _battleManager.TwistPoint.ToString("0");
        helmet.text = _battleManager.HelmetPoint.ToString("0");
        breakduration.text = _battleManager.BreakDuration.ToString("0");
        durationround.text = _battleManager.MaxTimerInSeconds.ToString("0");
        reactionTime.text = _battleManager.WindowTime.ToString("0.00");
        if (firstRound != null)
        {
            eachMember.text = _battleManager.NumberOfMembers.ToString("0");
            firstRound.text = _battleManager.FirstRoundDuration.ToString("0");
        }
    }
}
