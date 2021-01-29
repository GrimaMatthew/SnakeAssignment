using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static int snakeLength;

    public static GameManager inst;

    public static bool finishedlv1 = false;
    public static bool finishedlv2 = false;
    public static bool finishedlv3 = false;
    public static bool inlvl2 = false;
    public static bool inlvl1 = false;
    public static bool inlvl3 = false;
    bool timerset = true;

    GameObject timer;
    string sceneName;
    string namePlo;
    string PlayerName;
    public InputField nameTro;

    Text leaderboard;
 



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
      


        print(namePlo+" this is my name");
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;

        print("this is sceneName"+sceneName);

        loadLevels();
        setTimer();



        PlayerPrefs.SetFloat("Prefscore", timeManager.timerValue);


    }

    public void BtnLoadGame()
    {
         namePlo = nameTro.text;

        PlayerPrefs.SetString("Username", namePlo);
        SceneManager.LoadScene("Level1");
        inlvl1 = true;
    }

    private void loadLevels()
    {
        if (finishedlv1)
        {
            SceneManager.LoadScene("Level2");
            finishedlv1 = false;
            inlvl1 = false;

        }
        if (finishedlv2)
        {
            SceneManager.LoadScene("Level3");
            finishedlv2 = false;
            

        }

        if (finishedlv3)
        {

           
         
      
            SceneManager.LoadScene("End");
           
            finishedlv3 = false; 

            
        }
        if (sceneName == "Level1")
        {
            inlvl1 = true;
        }


        if (sceneName == "Level2")
        {
            inlvl2 = true;
        }


        if (sceneName == "Level3")
        {
            inlvl3 = true;
        }




    }

    private void setTimer()
    {
        if (sceneName != "Home" && timerset)
        {
            timer = Instantiate(Resources.Load<GameObject>("Timer"), new Vector3(0f, 0f), Quaternion.identity); // Get the timer from the resources file in the asset folder and create an instance of it
                                                                                                                // the default value for the timer is started
            timer.GetComponentInChildren<timeManager>().timerStarted = true; // Get the timer find the component on it named timeManager(script) which is attached to the timer
            DontDestroyOnLoad(timer);
            timerset = false;
            print("inside timer");
        }

        if(sceneName == "End")
        {
            Destroy(timer);

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
