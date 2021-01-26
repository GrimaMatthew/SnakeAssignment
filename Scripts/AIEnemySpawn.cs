using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemySpawn : MonoBehaviour
{
    Seeker seeker;

    Path pathToFollow;

    Transform target;


    GameObject enemybreadcrumbBox;



    List<positionRecord> enemyPastPositions;

 


    int enemyPositionOrder = 0;
    int enemyLength = 4;


    bool firstrun = true;





    void Start()
    {


        StartCoroutine(pathing());
        StartCoroutine(updateGridGraph());


        ///---->
        ///
  


        enemybreadcrumbBox = Resources.Load<GameObject>("Square"); // Get the sprite(Square) from the resources file in the asset folder and attach it to the Gameobject breadcrumb box
        enemybreadcrumbBox.name = "bread";

        this.gameObject.GetComponent<SpriteRenderer>().color = Color.grey; // Set the playerbox to black

        enemyPastPositions = new List<positionRecord>();

        drawTail(enemyLength);



        ///-->
    }

    private void Update()
    {
        savePosition();
        drawTail(enemyLength);

    }


    IEnumerator pathing() {


        yield return new WaitForSeconds(4f);


        AstarPath.active.Scan();
        target = GameObject.Find("Player").transform;



        seeker = GetComponent<Seeker>();


        pathToFollow = seeker.StartPath(this.transform.position, target.position);

        yield return seeker.IsDone();

        StartCoroutine(moveTowardsEnemy(this.transform)); 





    }

    IEnumerator updateGridGraph()
    {
        while (true)
        {
            AstarPath.active.Scan();
            yield return null;
        }

    }



    IEnumerator moveTowardsEnemy(Transform t)
    {

        print("in move enemy");
        while (true)
        {
            List<Vector3> posns = pathToFollow.vectorPath;

            for (int counter = 0; counter < posns.Count; counter++)

            {
                while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)

                {
                    t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                    yield return new WaitForSeconds(0.5f);
                    pathToFollow = seeker.StartPath(t.position, target.position);
                    yield return seeker.IsDone();
                    posns = pathToFollow.vectorPath;

                }

            }
        }
    }





    ////--->
    void clearLastBox() // This will clear the the box at the end of the tail
    {
        foreach (positionRecord p in enemyPastPositions)
        {

            Destroy(p.BreadcrumbBox); // Destroying the breadcrunbBox
        }
    }


    void drawTail(int length)
    {
        
        clearLastBox(); // If it is not run first the box will be added to snaketail so first we have to clear the last breadcrumb box 


        if (enemyPastPositions.Count > length) // if the pastposition list count (the amount of elements in the list) is larger than the length of the snake
        {

            int tailStartIndex = enemyPastPositions.Count - 1;  // setting the tail start box index (last breadbox box)
            int tailEndIndex = tailStartIndex - length; // Setting the last box index (breadbox box behind head)



            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--) // Starting at the startindex and going on until the startindex is larger than the tailEndIndex
            {

                enemyPastPositions[snakeblocks].BreadcrumbBox = Instantiate(enemybreadcrumbBox, enemyPastPositions[snakeblocks].Position, Quaternion.identity); // Instatiating the breadcrumb box at index tailstartIndex in the pastpositions list which is of type position record 
                enemyPastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow; // Setting the colour for the insta box to yellow 
            }
        }

        if (firstrun) // Since the first time round the pastpositioncount will be zero we will run this method to populate pastposition
        {

            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1; // Setting up the coordinate  to spawn the fakeboxpos
                fakeBoxPos.Position = new Vector3(0f, ycoord); //placing the facebokx pos 
                enemyPastPositions.Add(fakeBoxPos);// adding the fakebox pos to the list 
            }
            firstrun = false; // then swithch it off so that we don't we enter the other if statment
            drawTail(length); // And draw the tail 

        }

    }
    ///--->
    ///-->
    bool boxExists(Vector3 positionToCheck) // comparing postion to check with our pastPosition list
    {


        foreach (positionRecord p in enemyPastPositions) // traversing the pastPositions list 
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

        currentBoxPos.Position = this.gameObject.transform.position; // currentBoxPos Postions(postion in class position record) is set equal to the playerbox position
        enemyPositionOrder++;  // incriment position order 
        currentBoxPos.PositionOrder = enemyPositionOrder; // Position order for the current boxpos is set to position order

        if (!boxExists(this.gameObject.transform.position)) // boxExists our method returns True/False and is checking the playerbox.transform position and if it matches 
                                                      //This is run if it is false so the box doesnt exists 
        {
            currentBoxPos.BreadcrumbBox = Instantiate(enemybreadcrumbBox, this.gameObject.transform.position, Quaternion.identity); // if box doesnt exists instintiate a bread box  at playerbox position


            currentBoxPos.BreadcrumbBox.name = enemyPositionOrder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        enemyPastPositions.Add(currentBoxPos); // adding to the past positions
        Debug.Log("Have made this many moves: " + enemyPastPositions.Count);

    }
    //


}
               






 





   
