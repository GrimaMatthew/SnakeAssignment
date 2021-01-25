using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    SnakeGeneraor mysnakegenerator;


    


    private void Start()
    {
        mysnakegenerator = Camera.main.GetComponent<SnakeGeneraor>();

       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1f, 0);
            print("Topper");
            Debug.Log("Results From" + mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0);
            Debug.Log("Results From"+mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1f);
            Debug.Log("Results From" + mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 1f);
            Debug.Log("Results From" + mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength));
        }

    }


    public IEnumerator automoveCoroutine()
    {
        while (true)
        {


            yield return null;
        }

    }


}

