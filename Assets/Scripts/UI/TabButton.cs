using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup _tabGroup;
    [SerializeField] private Image _backGround;
    void Start()
    {
        _backGround = GetComponent<Image>();
        _tabGroup.Suscribe(this);
    }

    public void SetBackground(Sprite backGround)
    {
        _backGround.sprite = backGround;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      _tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
    }
}
