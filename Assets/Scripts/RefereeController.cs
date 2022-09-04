using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;


public class RefereeController : MonoBehaviour
{
    [SerializeField] private GameObject detectionManager = default;
    [SerializeField] private GameObject tableManager = default;
    [SerializeField] private bool[] redScoreDelay = default;
    [SerializeField] private bool[] blueScoreDelay = default;
    [SerializeField] private GameObject battleManager = default;
    [SerializeField] private float timerDelay = default;
    [SerializeField] private int username = default;
    [SerializeField] private GameObject loadingScreen = default;

    public event Action<PlayerInput> onDeviceLost;
    
    public void HelmetBlue(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!blueScoreDelay[2])
            {
                blueScoreDelay[2] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(2, false, username);
                StartCoroutine(ResetDelay(2, false));
            }
        }
    }
    public void HelmetRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!redScoreDelay[2])
            {
                redScoreDelay[2] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(2,true, username);
                StartCoroutine(ResetDelay(2, true));
            }

        }
    }
    public void ChestPlateRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!redScoreDelay[1])
            {
                redScoreDelay[1] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(1,true, username);
                StartCoroutine(ResetDelay(1, true));
            }
        }
        
    }
    public void ChestPlateBlue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!blueScoreDelay[1])
            {
                blueScoreDelay[1] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(1,false, username);
                StartCoroutine(ResetDelay(1, false));
            }
        }
       
    }
    public void PunchBlue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!blueScoreDelay[0])
            {
                blueScoreDelay[0] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(0,false, username);
                StartCoroutine(ResetDelay(0, false));
            }
        }
    }
    public void PunchRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!redScoreDelay[0])
            {
                redScoreDelay[0] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(0,true, username);
                StartCoroutine(ResetDelay(0, true));
            }
        }
    }
   /* public void TwistRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!redScoreDelay[3])
            {
                redScoreDelay[3] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(3,true, username);
                StartCoroutine(ResetDelay(3, true));
            }
        }
    }
    public void TwistBlue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!blueScoreDelay[3])
            {
                blueScoreDelay[3] = true;
                detectionManager.GetComponent<DetectionManager>().WindowIsOpened(3,false, username);
                StartCoroutine(ResetDelay(3, false));
            }
        }
    }
*/
    private IEnumerator ResetDelay(int action, bool isRed)
    {
        yield return new WaitForSeconds(timerDelay);
        if (isRed)
        {
            redScoreDelay[action] = false;
        }
        else
        {
            blueScoreDelay[action] = false;
        }
    }
    private void Start()
    {
        tableManager = GameObject.FindGameObjectWithTag("ScoreManager");
        detectionManager = GameObject.FindGameObjectWithTag("Counter");
        battleManager = GameObject.FindGameObjectWithTag("Data");
        tableManager.GetComponent<TableSystem>().FindTheControllers();
        timerDelay = battleManager.GetComponent<BattleManager>().WindowTime;
        username = GetComponent<PlayerInput>().playerIndex;
        detectionManager.GetComponent<DetectionManager>().AddReferee();
        loadingScreen = GameObject.FindGameObjectWithTag("Loading");
        if ( loadingScreen != null)
        {
            loadingScreen.GetComponent<LoadingScreen>().AddReferee();
        }
    }
    
}
