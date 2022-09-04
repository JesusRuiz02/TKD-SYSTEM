using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _canvas = default;
    [SerializeField] private GameObject _timer = default;
    [SerializeField] private GameObject[] symbolController = default;
    [SerializeField] private Sprite _controller = default;
    [SerializeField] private int _numberOfReferee = 0;

    public void AddReferee()
    {
        _numberOfReferee++;
    }

    public void StartCombat()
    {
        _canvas.SetActive(false);
        gameObject.SetActive(false);
        _timer.GetComponent<Timer>().enabled = true;
    }
    void Update()
    {
        switch (_numberOfReferee)
        {
            case 1:
                symbolController[0].GetComponent<Image>().sprite = _controller;
                break;
            case 2:
                symbolController[1].GetComponent<Image>().sprite = _controller;
                break;
            case 3:
                symbolController[2].GetComponent<Image>().sprite = _controller;
                break;
            case 4:
                symbolController[3].GetComponent<Image>().sprite = _controller;
                break;
            case 5:
                symbolController[4].GetComponent<Image>().sprite = _controller;
                break;
        }
    }
}
