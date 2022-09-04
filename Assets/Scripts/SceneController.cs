using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _1vs1 = default;
    [SerializeField] private GameObject _5vs5 = default;
    [SerializeField] private GameObject start = default;
   
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ActivatingCanvas()
    {
        _1vs1.SetActive(true);
        start.SetActive(false);
    }

    public void Activate2NdCanvas()
    {
        _5vs5.SetActive(true);
        start.SetActive(false);
    }
}
