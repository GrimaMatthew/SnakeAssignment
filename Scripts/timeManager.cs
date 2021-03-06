﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timeManager : MonoBehaviour
{

    public bool timerStarted;

    public static float timerValue = 0f;

    Text timerText;


    IEnumerator timer()
    {
        while (true)
        {
            if (timerStarted)
            {
                //measure the time
                timerValue++; // gamedata . timer++

                float minutes = timerValue / 120f; //120
                float seconds = timerValue % 60f;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;

            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        print("timeee in  timeee");
        //the text component attached to THIS object
        timerText = GetComponent<Text>();
        StartCoroutine(timer());
    }


}

