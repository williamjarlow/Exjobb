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
    List<GameObject> children = new List<GameObject>();
    [SerializeField]
    Color activeColor, inactiveColor, activeTextColor, inactiveTextColor;
    [SerializeField]
    float scrollDelay;

    int index = 0;
    bool glossaryOpen = true;
    bool scrollLock = false;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
            if(child.gameObject != gameObject)
                children.Add(child.gameObject);
        foreach (GameObject child in children)
        {
            if (child.CompareTag("Tab"))
            {
                Tab tab = new Tab();
                tab.container = child.gameObject;
                tabs.Add(tab);

            }
        }
        foreach(Tab tab in tabs)
        {
            foreach (Transform child in tab.container.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Toggleable"))
                    tab.content = child.gameObject;
                else if (child.CompareTag("Indicator"))
                {
                    tab.indicator = child.GetComponent<Image>();
                    tab.title = tab.indicator.GetComponentInChildren<Text>();
                }
            }
        }
        OpenTab(0);
        ToggleGlossary();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            index = 0;
            ToggleGlossary();
            OpenTab(index);
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && index > 0 && glossaryOpen && !scrollLock)
        {
            index--;
            scrollLock = true;
            StartCoroutine("Scroll");
            OpenTab(index);
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && index < tabs.Count - 1 && glossaryOpen && !scrollLock)
        {
            index++;
            scrollLock = true;
            StartCoroutine("Scroll");
            OpenTab(index);
        }

        
    }

    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(scrollDelay);
        scrollLock = false;
    }

    void ToggleGlossary()
    {
        if (glossaryOpen)
        {
            foreach (GameObject child in children)
            {
                child.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject child in children)
            {
                child.SetActive(true);
            }
        }
        glossaryOpen = !glossaryOpen;
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
