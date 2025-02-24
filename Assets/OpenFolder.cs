using System;
using UnityEngine;
using System.IO;
using SFB;

public class OpenFolder : MonoBehaviour
{
    private void Start()
    {
        SelectFolder();
    }

    private void SelectFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Seleccionar carpeta", "", false);
        if (paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
        {
            Debug.Log("No se seleccionó ninguna carpeta.");
            return;
        }

        string directoryPath = paths[0]; // Carpeta seleccionada

        // Crear carpeta si no existe
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

    }
    
}
