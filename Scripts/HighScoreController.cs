using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class HighScore
{
    public string rname;
    public float rtime;

  

    public HighScore(string pname, float ptime)
    {
        rname = pname;
        rtime = ptime;
    }
}


