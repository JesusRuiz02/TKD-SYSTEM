using UnityEngine;
using UnityEngine.InputSystem;

public class TableSystem : MonoBehaviour
{
    private TableManager _tableManager;
   [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private bool isPaused = default;
    private InputAction _inputPause = default;
    private InputAction _addRedGamJeom = default;
    private InputAction _addBlueGamJeom = default;
    [SerializeField] private GameObject pauseUI = default;
    [SerializeField] private GameObject[] _refereeList = default;
    [SerializeField] private GameObject _counterManager = default;

    private void OnEnable()
    {
        _inputPause = _tableManager.Table.Pause;
        _inputPause.Enable();
        _inputPause.performed += InputPause;
        _addBlueGamJeom = _tableManager.Table.AddBlueGamJeom;
        _addBlueGamJeom.Enable();
        _addBlueGamJeom.performed += AddBlueGamJeom;
        _addRedGamJeom = _tableManager.Table.AddRedGamJeom;
        _addRedGamJeom.Enable();
        _addRedGamJeom.performed += AddRedGamJeom;
    }

    private void OnDisable()
    {
        _inputPause.Disable();
        _addBlueGamJeom.Disable();
        _addRedGamJeom.Disable();
    }

    private void Awake()
    {
        _tableManager = new TableManager();
    }

    private void Start()
    {
        isPaused = false;
        _scoreManager = GetComponent<ScoreManager>();
        _counterManager = GameObject.FindGameObjectWithTag("Counter");
    }

    private void Update()
    {
        Pause();
        if (isPaused)
        {
            DisableReferee();
        }
        else
        {
            ActivateReferee();
        }
    }

    private void AddBlueGamJeom(InputAction.CallbackContext context)
    {
        _scoreManager.AddGamJeum(false);
    }
    private void AddRedGamJeom(InputAction.CallbackContext context)
    {
        _scoreManager.AddGamJeum(true);
    }
    private void InputPause(InputAction.CallbackContext context)
    {
        Toggle();
    }

    private void Toggle()
    {
        isPaused = !isPaused;
    }
    private void Pause()
    {
        var TimeScale = isPaused ? 0 : 1;
        Time.timeScale = TimeScale;
        pauseUI.SetActive(isPaused);
    }

    private void ActivateReferee()
    {
       _counterManager.GetComponent<DetectionManager>().PlayReferee();
    }
    public void FindTheControllers()
    {
        _refereeList = GameObject.FindGameObjectsWithTag("Referee");
    }
    private void DisableReferee()
    {
        _counterManager.GetComponent<DetectionManager>().StopReferee();
    }
}
