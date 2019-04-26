using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text timerText;
    float startTime;
    bool timerStarted;

    void Start()
    {
        timerText = GetComponent<Text>();
        startTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && !timerStarted)
        {
            startTime = Time.fixedTime;
            timerStarted = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            startTime = Time.fixedTime;
            timerStarted = false;
            timerText.text = (Time.fixedTime - startTime).ToString("F2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
            timerText.enabled = !timerText.enabled;

        if(timerStarted)
            timerText.text = (Time.fixedTime - startTime).ToString("F2");

        if(GameManager.instance.sceneIndex == 8)
            timerStarted = false;
    }
}
