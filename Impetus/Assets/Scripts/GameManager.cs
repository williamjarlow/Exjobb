using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    int targetCount = 1;
    [SerializeField]
    int sceneIndex = 1;
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
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void OnDummyDestroy()
    {
        targetCount--;
        Debug.Log(targetCount);
        if (targetCount == 0)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            FindObjectOfType<Movement>().enabled = false;
            FindObjectOfType<ManageSkills>().enabled = false;
            victoryText.SetActive(true);
            StartCoroutine("LoadNextScene");
        }
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
        victoryText.SetActive(false);
        targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
    }
}
