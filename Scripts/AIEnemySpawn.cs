using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemySpawn : MonoBehaviour
{
    Seeker seeker;

    Path pathToFollow;

    Transform target;


    GameObject enemybread;



    List<positionRecord> enemyPastPositions;

 


    int enemyPositionOrder = 0;
    public  static int enemyLength = 3;


    bool firstrun = true;





    void Start()
    {


        StartCoroutine(pathing());
        StartCoroutine(updateGridGraph());
    


        enemybread = Resources.Load<GameObject>("Square"); // Get the sprite(Square) from the resources file in the asset folder and attach it to the Gameobject breadcrumb box 

        this.gameObject.GetComponent<SpriteRenderer>().color = Color.grey; // Set the playerbox to black

        enemybread.name = "bread";

        enemyPastPositions = new List<positionRecord>();

    }



    IEnumerator pathing() {


        yield return new WaitForSeconds(2f);


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
                    savePosition();
                    enemydrawTail(enemyLength);
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

            Destroy(p.enemyBreadcrumbBox); // Destroying the breadcrunbBox
        }
    }


    void enemydrawTail(int length)
    {
        clearLastBox();




        if (enemyPastPositions.Count >= length) // if the pastposition list count (the amount of elements in the list) is larger than the length of the snake
        {
    

            int tailStartIndex = enemyPastPositions.Count - 1;  // setting the tail start box index (last breadbox box)
            int tailEndIndex = tailStartIndex - length; // Setting the last box index (breadbox box behind head)



            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--) // Starting at the startindex and going on until the startindex is larger than the tailEndIndex
            {

                enemyPastPositions[snakeblocks].enemyBreadcrumbBox = Instantiate(enemybread, enemyPastPositions[snakeblocks].Position, Quaternion.identity); // Instatiating the breadcrumb box at index tailstartIndex in the pastpositions list which is of type position record 


                enemyPastPositions[snakeblocks].enemyBreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.cyan; // Setting the colour for the insta box to yellow 
            }
        }



        if (firstrun) // Since the first time round the pastpositioncount will be zero we will run this method to populate pastposition
        {

            for (int count = length; count > 0; count--)
            {

           

                positionRecord efakeBoxPos = new positionRecord();
                float ycoord = count ; // Setting up the coordinate  to spawn the fakeboxpos
                efakeBoxPos.Position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y); //placing the facebokx pos 
                enemyPastPositions.Add(efakeBoxPos);// adding the fakebox pos to the list
                print("inside first run");
               
            }

            firstrun = false; // then swithch it off so that we don't we enter the other if statment
            enemydrawTail(enemyLength); // And draw the tail



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

                if (p.enemyBreadcrumbBox != null) // and if there is a Breadcrumbox 
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
            print("inside save");
            currentBoxPos.enemyBreadcrumbBox = Instantiate(enemybread, this.transform.position, Quaternion.identity);  // if box doesnt exists instintiate a bread box  at playerbox position

            currentBoxPos.enemyBreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        enemyPastPositions.Add(currentBoxPos); // adding to the past positions
        Debug.Log("Have made this many moves: " + enemyPastPositions.Count);

    }
    //


}
               






 





   
