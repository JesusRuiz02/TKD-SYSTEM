using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PairManager : MonoBehaviour
{
    private static PairManager _instance;
    public static PairManager GetInstance()
    {
        return _instance;
    }
    [SerializeField] private DetectionManager _detectionManager;
    [SerializeField] List<GameObject>  _refereeList = new List<GameObject>();
    [SerializeField] private GameObject PairCanvas;
    private LoadingScreen _loadingScreen;
    [SerializeField] private PlayerInputManager _playerInputManager;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (PairCanvas) _playerInputManager.enabled = true;
        _loadingScreen = PairCanvas.GetComponentInChildren<LoadingScreen>();
    }
    
    
    public void ResetControllers()
    {
        foreach (GameObject controller in _refereeList)
        {
            UnpairedPlayers( controller.GetComponent<PlayerInput>());
            Destroy(controller);
        }
        GameManager.GetInstance().ChangeCombatState(CombatStates.PAIRING_STATE);
        _refereeList.Clear();
        _loadingScreen.PairedCheck();
        _detectionManager.SetNumberOfJudges(0);
     
    }
    public void FindTheControllers(GameObject controller)
    {
        _refereeList.Add(controller);
    }
    

    private void UnpairedPlayers(PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
    }
}
