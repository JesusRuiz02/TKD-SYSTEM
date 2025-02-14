using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] symbolController = default;
    [SerializeField] private Sprite _controller = default;
    [SerializeField] private Sprite _emptyController =  default;
    [SerializeField] private int _numberOfReferee = 0;
    

    public void AddReferee()
    {
        _numberOfReferee++;
        RefereeCheck();
    }

    public void PairedCheck()
    {
        _numberOfReferee = 0;
        RefereeCheck();
    }

    public void RefereeCheck()
    {
        switch (_numberOfReferee)
        {
            case 0:
                symbolController[0].GetComponent<Image>().sprite = _emptyController;
                symbolController[1].GetComponent<Image>().sprite = _emptyController;
                symbolController[2].GetComponent<Image>().sprite = _emptyController;
                symbolController[3].GetComponent<Image>().sprite = _emptyController;
                symbolController[4].GetComponent<Image>().sprite = _emptyController;
                break;
            
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
