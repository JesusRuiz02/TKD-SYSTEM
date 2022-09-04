using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Password : MonoBehaviour
{
   [SerializeField] private TMP_InputField inputfield;  
 
    public void CheckInput() {
        if (inputfield.text == "BFGD-2310-LCME-2407")      // check inputfield contains the string password
        {
            Debug.Log("Password accepted");     // just a debug.Log to show that the password is correct (can be removed)
            SceneManager.LoadScene("MainMenu");  // fill in the name of the scene you want to load
        }
    }

    private void FixedUpdate()
    {
        CheckInput();
    }
}
