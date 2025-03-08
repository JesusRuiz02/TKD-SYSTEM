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
        ShowText();
    }
    
    public void ShowText()
    {
        if (punch != null)
        {
             punch.text = _battleManager.PunchPoint.ToString("0");
        }
        if (rounds != null)
        {
            rounds.text = _battleManager.NumberOfRounds.ToString("0");
        }
        if (difference != null)
        {
            difference.text = _battleManager.PointDifference.ToString("0");
        }
        if (gameJeum != null)
        {
            gameJeum.text = _battleManager.GameJeum.ToString("0");
        }
        if (chestPlate != null)
        {
            chestPlate.text = _battleManager.ChestPlatePoint.ToString("0");
        }
        if (twist != null)
        {
            twist.text = _battleManager.TwistPoint.ToString("0");
        }
        if (helmet != null)
        {
            helmet.text = _battleManager.HelmetPoint.ToString("0");
        }
        if (breakduration != null)
        {
            breakduration.text = _battleManager.BreakDuration.ToString("0");
        }
        if (durationround != null)
        {
            durationround.text = _battleManager.MaxTimerInSeconds.ToString("0");
        }
        if (reactionTime != null)
        {
            reactionTime.text = _battleManager.WindowTime.ToString("0.00");
        }
        if (firstRound != null)
        {
            eachMember.text = _battleManager.NumberOfMembers.ToString("0");
            firstRound.text = _battleManager.FirstRoundDuration.ToString("0");
        }
    }
}
