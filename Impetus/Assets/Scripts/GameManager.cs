using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [HideInInspector]
    public int dummiesDestroyed = 0;
    private int sceneIndex = 1;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = gameObject.GetComponent<GameManager>();
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
        dummiesDestroyed++;
        StartCoroutine("LoadNextScene");
    }

    IEnumerable LoadNextScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(++sceneIndex);
    }

}
