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
            checkBounds();
            if(mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength))
            {
                GameManager.lostGame = true;

            };
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0);
            checkBounds();
            if (mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength))
            {
                GameManager.lostGame = true;

            };
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1f);
            checkBounds();
            if (mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength))
            {
                GameManager.lostGame = true;

            };
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 1f);
            checkBounds();
            if (mysnakegenerator.hitTail(this.transform.position, GameManager.snakeLength))
            {
                GameManager.lostGame = true;

            };
        }

    }



    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize - 1)) || (transform.position.x > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 1)) || (transform.position.y > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
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

