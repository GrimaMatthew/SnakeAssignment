﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
 

    // Start is called before the first frame update
    void Start()
    {

        Camera.main.GetComponent<SnakeGeneraor>().enabled = true;
   




    }
}
