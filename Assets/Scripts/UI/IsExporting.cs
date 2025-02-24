using System;
using UnityEngine;
using UnityEngine.UI;

public class IsExporting : MonoBehaviour
{
   public Toggle toggle;

   private void Start()
   {
      int value = PlayerPrefs.GetInt("IsExport", 1);
      toggle.isOn = value == 1;
      toggle.onValueChanged.AddListener(ToggleOnChange);
   }

   void ToggleOnChange(bool state)
   {
      PlayerPrefs.SetInt("IsExport", toggle ? 1 : 0);
   }
}
