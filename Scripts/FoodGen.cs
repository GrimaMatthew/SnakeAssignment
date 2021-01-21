using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FoodGen : MonoBehaviour
{
    //public List<GameObject> ovs;

    GameObject Player;
    GameObject Target;
    GameObject food;
    Seeker seeker;

    List<GameObject> foodKist = new List<GameObject>();

    Path pathToFollow;


    int firstTime = 4;


    // Start is called before the first frame update
    void Start()
    {

      
        
        Target = GameObject.Find("Diamond");


        seeker = GetComponent<Seeker>();




        StartCoroutine(checkFood());

        StartCoroutine(generatefood(true));


    }

    // Update is called once per frame
    


    IEnumerator checkFood()
    {
        yield return new WaitForSeconds(2f);

        Player = GameObject.Find("Player");
        print(Player.transform.position);

        pathToFollow = seeker.StartPath(Player.transform.position,Target.transform.position );
        Player.GetComponent<SpriteRenderer>().color = Color.red;

        print("Transforms: " + Player.transform.position + "Waypoints: " + Target.transform.position);

        yield return seeker.IsDone();
        print("CheckFoodRun");
        StartCoroutine(updateGridGraph());


    }

    IEnumerator updateGridGraph()
    {
        while (true)
        {
            AstarPath.active.Scan();
            yield return null;
        }

    }


    IEnumerator generatefood(bool loop)
    {

        yield return new WaitForSeconds(4f);
        List<Vector3> posn = pathToFollow.vectorPath;
        print(posn.Count + "Count");
        if(loop)
        {
            while (true)
            {
                for (int i = 0; i <= posn.Count; i ++)
                {


                    print(posn[i] + "printing for posn[i]");
                    food = Instantiate(Resources.Load<GameObject>("Square"), posn[i+firstTime], Quaternion.identity);  
                    food.GetComponent<SpriteRenderer>().color = Color.green;
                    print(posn[i + 4] + "printing for posn[i+4]");
                    print(i+"this is the i value posn");
                    firstTime++;
                    food.name = "Food";
                    yield return new WaitForSeconds(3f);
                    foodKist.Add(food);
                 
                   

                    }
                

                    

                }

            }
        yield return null;

    }
       
       
    }

   /*

    IEnumerator checkFoodpos() {


        while(true)
        {
            foreach (GameObject p in ovs)
            {
                float dist = Vector3.Distance(p.transform.position, food.transform.position);
                print("Distance: "+dist);

                if (dist > 3f)
                {
                    food = Instantiate(Resources.Load<GameObject>("Square"), new Vector3(Random.Range(9f,-9f), Random.Range(9f, -9f)), Quaternion.identity);
                    food.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {

                  

                }
                yield return  new WaitForSeconds(2f);


            }

        }
     





    }


    


   bool ObstExists() // comparing postion to check with our pastPosition list
    {


        foreach (GameObject p in ovs) // traversing the pastPositions list 
        {
            print("Obstacle" + p.transform.position + "to check" + food.transform.position);

            if (p.transform.position == food.transform.position) // if the p.postions matches the postionToCeck
            {
               
              print("Hittt");

            }

            print("ovs(p)" + p.transform.position);
            print("Checking Green Box" + food.transform.position);
        }
      
        print("Obstaclii not hittt");

        return false;
    }

    */


