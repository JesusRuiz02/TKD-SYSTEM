using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Password : MonoBehaviour
{
 
   [SerializeField] private TMP_InputField _eventNameField; 
   [SerializeField] private TMP_InputField _areaNumberField;
   [SerializeField] private Animation _areaAnimation;
   [SerializeField] private Animation _eventAnimation;
    public void CheckInput() {
        if (_eventNameField.text != "" && _areaNumberField.text != "")
        {
            SceneManager.LoadScene("MainMenu");
            PlayerPrefs.SetString("EventName",_eventNameField.text);
            PlayerPrefs.SetInt("AreaNumber", int.Parse(_areaNumberField.text));
        }
        else
        {
            if (_eventNameField.text == "") _eventAnimation.Play();
            if (_areaNumberField.text == "") _areaAnimation.Play();
        }
       
    }

    private void Start()
    {
        _areaNumberField.contentType = TMP_InputField.ContentType.IntegerNumber;
        _areaNumberField.characterLimit = 2;
    }
}
