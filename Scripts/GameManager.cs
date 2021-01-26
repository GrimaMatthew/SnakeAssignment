using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int snakeLength;

    public static GameManager inst;

    public static bool finishedlv1 = false;
    bool timerset = true;

    GameObject timer;
    string sceneName;


    private void Start()
    {
        
    }


    //----->

    private void Awake()
    {
        Singleton();


    }

    private void Update()
    {
        

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;

        print("this is sceneName"+sceneName);

        loadLevels();
        setTimer();


    }

    public void BtnLoadGame()
    {

        SceneManager.LoadScene("Level1");
    }

    private void loadLevels()
    {
        if (finishedlv1)
        {
            SceneManager.LoadScene("Level2");
            finishedlv1 = false;

        }
    }

    private void setTimer()
    {
        if (sceneName == "Level1" && timerset)
        {
            timer = Instantiate(Resources.Load<GameObject>("Timer"), new Vector3(0f, 0f), Quaternion.identity); // Get the timer from the resources file in the asset folder and create an instance of it
                                                                                                                // the default value for the timer is started
            timer.GetComponentInChildren<timeManager>().timerStarted = true; // Get the timer find the component on it named timeManager(script) which is attached to the timer
            timerset = false;
            print("inside timer");
        }

        print("timer" + timerset);
        


    }




    private void Singleton()
    {
        if (inst != null)
        {
            Destroy(gameObject);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
