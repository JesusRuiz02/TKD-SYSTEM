using UnityEngine;
using SFB;

public class OpenFolder : MonoBehaviour
{
    public void SelectFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Seleccionar carpeta", "", false);
        if (paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
        {
            Debug.Log("No se seleccionó ninguna carpeta.");
            return;
        }
        string directoryPath = paths[0]; 
        PlayerPrefs.SetString("PathToExport", directoryPath);
    }
    
}
