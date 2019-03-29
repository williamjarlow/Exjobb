using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Tab
{
    [HideInInspector]
    public GameObject container, content;
    [HideInInspector]
    public Text title;
    [HideInInspector]
    public Image indicator;

}

public class GlossaryController : MonoBehaviour
{
    List<Tab> tabs = new List<Tab>();

    [SerializeField]
    Color activeColor, inactiveColor, activeTextColor, inactiveTextColor;

    int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform transform in GetComponentsInChildren<Transform>())
        {
            if (transform.CompareTag("Tab"))
            {
                Tab tab = new Tab();
                tab.container = transform.gameObject;
                tabs.Add(tab);

            }
        }
        foreach(Tab tab in tabs)
        {
            foreach (Transform transform in tab.container.GetComponentsInChildren<Transform>())
            {
                if (transform.CompareTag("Toggleable"))
                    tab.content = transform.gameObject;
                else if (transform.CompareTag("Indicator"))
                {
                    tab.indicator = transform.GetComponent<Image>();
                    tab.title = tab.indicator.GetComponentInChildren<Text>();
                }
            }
        }
        OpenTab(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && index > 0)
        {
            index--;
            OpenTab(index);
        }
        else if (Input.GetButtonDown("Clone") && index < tabs.Count - 1)
        {
            index++;
            OpenTab(index);
        }
        
    }

    void OpenTab(int index = 0)
    {
        CloseTabs();
        tabs[index].indicator.color = activeColor;
        tabs[index].title.color = activeTextColor;
        tabs[index].content.SetActive(true);
    }

    void CloseTabs()
    {
        foreach (Tab tab in tabs)
        {
            tab.indicator.color = inactiveColor;
            tab.title.color = inactiveTextColor;
            tab.content.SetActive(false);
        }
    }

}
