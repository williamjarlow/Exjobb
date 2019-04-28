using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Timer : MonoBehaviour
{
    [SerializeField]
    List<Text> splitTexts, descriptorTexts;
    [SerializeField]
    Image background;
    Text timerText;
    float startTime;
    float splitStartTime;
    bool timerStarted;

    void Start()
    {
        timerText = GetComponent<Text>();
        SetTextEnable(false);
        startTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !timerStarted)
        {
            startTime = Time.time;
            timerStarted = true;
            StartSplit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            startTime = Time.time;
            timerStarted = false;
            timerText.text = (Time.time - startTime).ToString("F2");
            foreach (Text text in splitTexts)
                text.text = "0.00";
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
            timerText.enabled = !timerText.enabled;

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetTextEnable(!splitTexts[0].enabled);
        }

        if (timerStarted)
            timerText.text = (Time.time - startTime).ToString("F2");

        if(GameManager.instance.sceneIndex == 8)
            timerStarted = false;
    }

    void StartSplit()
    {
        if (GameManager.instance.sceneIndex != 0)
        {
            splitTexts[GameManager.instance.sceneIndex - 1].text = "0.00";
            splitStartTime = Time.time;
        }
    }

    void SetTextEnable(bool value)
    {
        timerText.enabled = value;
        background.enabled = value;
        foreach (Text text in descriptorTexts)
            text.enabled = value;
        foreach (Text text in splitTexts)
            text.enabled = value;
    }

    public void NextSplit()
    {
        if (GameManager.instance.sceneIndex != 1)
            splitTexts[GameManager.instance.sceneIndex - 2].text = (Time.time - splitStartTime).ToString("F2");
        if (GameManager.instance.sceneIndex < 8)
        {
            StartSplit();
        }
        else
        {
            float total = 0;
            string fileString = "";

            foreach (Text text in splitTexts)
                total += float.Parse(text.text);
            timerText.text = total.ToString("F2");

            string fileName = System.DateTime.Now.ToShortDateString() + "__" +  System.DateTime.Now.ToShortTimeString();
            fileName = fileName.Replace('/', '_');
            fileName = fileName.Replace(' ', '_');
            fileName = fileName.Replace(':', ';');

            while (File.Exists(fileName + ".txt"))
                fileName += "a";

            var file = File.Open(fileName + ".txt", FileMode.CreateNew, FileAccess.ReadWrite);
            var writer = new StreamWriter(file);

            foreach (Text text in splitTexts)
            {
                fileString = text.text;
                writer.WriteLine(fileString);
            }
            fileString = timerText.text;
            writer.WriteLine(fileString);

            writer.WriteLine(" ");
            writer.Close();
        }
    }
}
