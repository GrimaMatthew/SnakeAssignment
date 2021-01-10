using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//---> Class

public class positionRecord
{
 
    Vector3 position;

   
    int positionOrder;

    GameObject breadcrumbBox;


    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}

//---> Class


// ----> SnakeGenerator

public class SnakeGeneraor : MonoBehaviour
{

    public int snakelength=3;
     

    GameObject playerBox, breadcrumbBox, pathParent;

    List<positionRecord> pastPositions;

    int positionorder = 0;


    bool firstrun = true;


    void Start()
    {
        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        playerBox = Instantiate(Resources.Load<GameObject>("Square"), new Vector3(0f, 0f), Quaternion.identity);

        breadcrumbBox = Resources.Load<GameObject>("Square");

        playerBox.GetComponent<SpriteRenderer>().color = Color.black;

     
        playerBox.AddComponent<SnakeHeadController>();

        playerBox.name = "Player";

        pastPositions = new List<positionRecord>();

        drawTail(snakelength);

    }

    void Update()
    {
        if (Input.anyKeyDown && !((Input.GetMouseButtonDown(0)
          || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("a key was pressed " + Time.time);

            savePosition();

            drawTail(snakelength);
        }

    }

    // ----> SnakeGenerator





    // ----->Cour

    void clearTail()
    {
        foreach (positionRecord p in pastPositions)
        {
        
            Destroy(p.BreadcrumbBox);
        }
    }


    void drawTail(int length)
    {
        clearTail();
        print("in taillllll");

        if (pastPositions.Count > length)
        {
            
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;


    
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
  
                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        if (firstrun)
        {

            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
                pastPositions.Add(fakeBoxPos);
            }
            firstrun = false;
            drawTail(length);
          
        }

    }

    bool boxExists(Vector3 positionToCheck)
    {
   

        foreach (positionRecord p in pastPositions)
        {

            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    
              
                    return true;
                }
            }
        }

        return false;
    }

    void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = playerBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(playerBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, playerBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }

}
