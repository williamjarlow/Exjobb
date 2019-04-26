using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    int targetCount = 1;
    int maxTargets;
    public int sceneIndex = 1;
    float waitTime = 0;
    GameObject victoryText;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = gameObject.GetComponent<GameManager>();
        StartCoroutine("LoadNextScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            waitTime = 0;
            sceneIndex--;
            StartCoroutine("LoadNextScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            waitTime = 0;
            sceneIndex -= 2;
            sceneIndex = sceneIndex <= 0 ? 0 : sceneIndex;
            StartCoroutine("LoadNextScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            waitTime = 0;
            sceneIndex = sceneIndex >= 8 ? 7 : sceneIndex;
            StartCoroutine("LoadNextScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            waitTime = 0;
            sceneIndex = 0;
            StartCoroutine("LoadNextScene");
        }
    }

    public void OnDummyDestroy()
    {
        targetCount--;
        if (targetCount == 0)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            FindObjectOfType<Movement>().enabled = false;
            FindObjectOfType<ManageSkills>().enabled = false;
            GameObject.FindWithTag("Player").GetComponentInChildren<ParticleSystem>().Stop();

            victoryText.SetActive(true);
            victoryText.GetComponent<Text>().text = "Good job!";
            StartCoroutine("LoadNextScene");
        }
        else
            StartCoroutine("DisplayTargetProgress");
    }

    IEnumerator DisplayTargetProgress()
    {
        victoryText.SetActive(true);
        victoryText.GetComponent<Text>().text = (maxTargets - targetCount).ToString() + " / " + maxTargets;
        yield return new WaitForSeconds(1);
        GameObject.FindWithTag("Player").transform.position = new Vector3();
        victoryText.SetActive(false);
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTime);
        waitTime = 2;
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(++sceneIndex, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        };
        victoryText = GameObject.FindWithTag("VictoryText");
        if(victoryText != null)
            victoryText.SetActive(false);
        targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
        maxTargets = targetCount;
    }
}
