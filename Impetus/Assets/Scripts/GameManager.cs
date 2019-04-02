using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    int sceneIndex = 1;

    GameObject victoryText;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = gameObject.GetComponent<GameManager>();
    }

    private void Start()
    {
        victoryText = GameObject.FindWithTag("VictoryText");
        victoryText.SetActive(false);
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
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        FindObjectOfType<Movement>().enabled = false;
        FindObjectOfType<ManageSkills>().enabled = false;
        victoryText.SetActive(true);
        StartCoroutine("LoadNextScene");
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(++sceneIndex);
    }

    private void OnLevelWasLoaded(int level)
    {
        victoryText = GameObject.FindWithTag("VictoryText");
        victoryText.SetActive(false);
    }
}
