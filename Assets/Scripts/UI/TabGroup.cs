using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons = new List<TabButton>();
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;
    [SerializeField] private List<GameObject> _objectsToSwap;
    [SerializeField] private TabButton selectedTab;
    
    
    public void Suscribe(TabButton tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
          button.SetBackground(tabHover);
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.SetBackground(tabActive);
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < _objectsToSwap.Count; i++)
        {
            _objectsToSwap[i].SetActive(i == index);
        }
    }

    private void ResetTabs()
    {
        foreach (TabButton tab in tabButtons)
        {
            if (selectedTab != null && tab == selectedTab)
            {
                continue;
            }
            tab.SetBackground(tabIdle);
        }
    }

}
