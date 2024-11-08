using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int whatAmI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whatAmI == 1) 
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        }
        else if (whatAmI == 2) 
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 4f);
        }
        else if(whatAmI == 3) 
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(3f, 6f));
        }
        else if(whatAmI == 4)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(3f, 6f));
        }

        if ((transform.position.y > 9f || transform.position.y <= -9f) && whatAmI != 3)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y <= -9f &&  whatAmI == 3)
        {
            transform.position = new Vector3(Random.Range(-12f, 12f), 9f, 0);
        }
    }
}
