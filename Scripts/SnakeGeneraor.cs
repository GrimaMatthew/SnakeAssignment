﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//---> Class

public class positionRecord
{
 
    Vector3 position; // The position of where the snake has been 

   
    int positionOrder; //  

    GameObject breadcrumbBox; // Breadcrumbox used to leave a breadcrumb trail after the snake
    GameObject enemybreadcrumbBox; // Breadcrumbox used to leave a breadcrumb trail after the snake 

    //Setting getters and setters as a security not to alter it from the inspector 
    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
    public GameObject enemyBreadcrumbBox { get => enemybreadcrumbBox; set => enemybreadcrumbBox = value; }


}

//---> Class


// ----> SnakeGenerator

public class SnakeGeneraor : MonoBehaviour
{

   // A public variable to set the size/length of the snake
     

    GameObject playerBox, breadcrumbBox, pathParent,target;



    List<positionRecord> pastPositions;



    public List<GameObject> obstic = new List<GameObject>();
    

    int positionorder = 0;


    bool firstrun = true;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1;


    void Start()
    {
      

       



        target = GameObject.Find("Diamond");

    

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        playerBox = Instantiate(Resources.Load<GameObject>("Square"), new Vector3(0f, 0f), Quaternion.identity); // Get the sprite(Square) from the resources file in the asset folderand create an instance of it to the player box 


        breadcrumbBox = Resources.Load<GameObject>("Square"); // Get the sprite(Square) from the resources file in the asset folder and attach it to the Gameobject breadcrumb box 

        playerBox.GetComponent<SpriteRenderer>().color = Color.black; // Set the playerbox to black

     

     
        playerBox.AddComponent<SnakeHeadController>(); //  the snake head controller is added to the playerbox for the player to be able to move the head of the player

        playerBox.name = "Player"; // Setting the name to Player

        pastPositions = new List<positionRecord>();

        drawTail(GameManager.snakeLength); //If the drawtail is removed from the start the first box behind the sneakhed won't be drawn 

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
         

            savePosition();

            drawTail(GameManager.snakeLength);
        }


        foreach (GameObject o in obstic)
        {
            float dist = Vector3.Distance(playerBox.transform.position, o.transform.position);
            print("trop1: " + dist);

            if (dist<=1 )
            {

                GameManager.lostGame = true;
                print("Lost"+GameManager.lostGame);

            }
         
        }

        float disttoGoal = Vector3.Distance(playerBox.transform.position, target.transform.position);
        print("Distance to goal: "+disttoGoal);
        if (disttoGoal<=2 && GameManager.inlvl1 && GameManager.snakeLength ==6)
        {
            GameManager.finishedlv1 = true;

        }

        if(disttoGoal <= 2 && GameManager.inlvl2)
        {
            GameManager.finishedlv2 = true;


        }

        if (disttoGoal <= 2 && GameManager.inlvl3)
        {
            GameManager.finishedlv3 = true; 


        }


    }

    // ----> SnakeGenerator





    // ----->Cour & Methods




    void clearLastBox() // This will clear the the box at the end of the tail
    {
        foreach (positionRecord p in pastPositions)
        {
        
            Destroy(p.BreadcrumbBox); // Destroying the breadcrunbBox
        }
    }


    void drawTail(int length)
    {
        clearLastBox(); // If it is not run first the box will be added to snaketail so first we have to clear the last breadcrumb box 


        if (pastPositions.Count > length) // if the pastposition list count (the amount of elements in the list) is larger than the length of the snake
        {
            
            int tailStartIndex = pastPositions.Count - 1;  // setting the tail start box index (last breadbox box)
            int tailEndIndex = tailStartIndex - length; // Setting the last box index (breadbox box behind head)


    
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--) // Starting at the startindex and going on until the startindex is larger than the tailEndIndex
            {
  
                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity); // Instatiating the breadcrumb box at index tailstartIndex in the pastpositions list which is of type position record 
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow; // Setting the colour for the insta box to yellow 
            }
        }

        if (firstrun) // Since the first time round the pastpositioncount will be zero we will run this method to populate pastposition
        {

            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1; // Setting up the coordinate  to spawn the fakeboxpos
                fakeBoxPos.Position = new Vector3(0f, ycoord); //placing the facebokx pos 
                pastPositions.Add(fakeBoxPos);// adding the fakebox pos to the list 
            }
            firstrun = false; // then swithch it off so that we don't we enter the other if statment
            drawTail(length); // And draw the tail 
          
        }

    }

    public bool hitTail(Vector3 headPosition, int length)
    {
        int tailStartIndex = pastPositions.Count - 1;
        int tailEndIndex = tailStartIndex - length;

        //I am checking all the positions in the tail of the snake
        for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
        {
            if ((headPosition == pastPositions[snakeblocks].Position) && (pastPositions[snakeblocks].BreadcrumbBox != null))
            {
               
                 Debug.Log("Hit Tail");
                return true;
            }
        }


        return false;

    }


    


    bool boxExists(Vector3 positionToCheck) // comparing postion to check with our pastPosition list
    {
   

        foreach (positionRecord p in pastPositions) // traversing the pastPositions list 
        {

            if (p.Position == positionToCheck) // if the p.postions matches the postionToCeck
            {
        
                if (p.BreadcrumbBox != null) // and if there is a Breadcrumbox 
                {
                    
              
                    return true; // return true 
                }
            }
        }

        return false;
    }

    void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord(); 

        currentBoxPos.Position = playerBox.transform.position; // currentBoxPos Postions(postion in class position record) is set equal to the playerbox position
        positionorder++;  // incriment position order 
        currentBoxPos.PositionOrder = positionorder; // Position order for the current boxpos is set to position order

        if (!boxExists(playerBox.transform.position)) // boxExists our method returns True/False and is checking the playerbox.transform position and if it matches 
            //This is run if it is false so the box doesnt exists 
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, playerBox.transform.position, Quaternion.identity); // if box doesnt exists instintiate a bread box  at playerbox position

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform); // 

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos); // adding to the past positions
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }

}
