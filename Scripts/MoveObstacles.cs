using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveObsti());

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveObsti()
    {
        //create a new list of positions.
        List<Vector3> positions = new List<Vector3>();
      
        positions.Add(this.transform.position);

        positions.Add(new Vector3(this.transform.position.x, -this.transform.position.y));

        StartCoroutine(moveObsti(this.transform, positions, true));



        yield return null;
    }

    IEnumerator moveObsti(Transform t, List<Vector3> points, bool loop)
    {
        if (loop)
        {
            //needs to run indefinitely
            while (true)
            {
                List<Vector3> forwardpoints = points;

                foreach (Vector3 position in forwardpoints)
                {
                    while (Vector3.Distance(t.position, position) > 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, position, 1f);
                        Debug.Log(position);/**/
                        yield return new WaitForSeconds(1f);
                    }
                }
                //reverse the points supplied here
                forwardpoints.Reverse();
                yield return null;

            }
        }
        else
        {
            foreach (Vector3 position in points)
            {
                while (Vector3.Distance(t.position, position) > 0.5f)
                {
                    t.position = Vector3.MoveTowards(t.position, position, 1f);
                    /**/
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return null;
        }


    }
}
