using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int snakeLength;

    public static GameManager inst;

    public static bool finishedlv1 = false;



 
    //----->

    private void Awake()
    {
        Singleton();


    }

    private void Update()
    {
        

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        print("this is sceneName"+sceneName);

        loadLevels();


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
